using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.ProductCommand
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IProductRepository productRepository = null;
        public DeleteCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await productRepository.Delete(UpdateBuild(new MDM.UI.Products.Models.Product()
            {
                Id = request.ProductId
            }, request.LoginSession));
        }
    }
}
