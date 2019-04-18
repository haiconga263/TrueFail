using Common.Exceptions;
using DAL;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.ProductCommands
{
    public class DeleteProductCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteProductCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteProductCommandHandler : BaseCommandHandler<DeleteProductCommand, int>
    {
        private readonly IProductRepository productRepository = null;
        private readonly IProductQueries productQueries = null;
        public DeleteProductCommandHandler(IProductRepository productRepository, IProductQueries productQueries)
        {
            this.productRepository = productRepository;
            this.productQueries = productQueries;
        }
        public override async Task<int> HandleCommand(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Product.NotSelected");
            }
            else
            {
                product = await productQueries.GetByIdAsync(request.Model);
                if (product == null)
                    throw new BusinessException("Product.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        product.IsDeleted = true;
                        product.ModifiedDate = DateTime.Now;
                        product.ModifiedBy = request.LoginSession.Id;

                        if (await productRepository.UpdateAsync(product) > 0)
                            rs = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return rs;
        }
    }
}
