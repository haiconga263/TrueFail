using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.ProductCommand
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IProductRepository productRepository = null;
        private readonly IProductQueries productQueries = null;
        public AddCommandHandler(IProductRepository productRepository, IProductQueries productQueries)
        {
            this.productRepository = productRepository;
            this.productQueries = productQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        productRepository.JoinTransaction(conn, trans);
                        productQueries.JoinTransaction(conn, trans);

                        if (request.Product == null)
                        {
                            throw new BusinessException("AddWrongInformation");
                        }

                        var product = (await productQueries.Gets($"p.code = '{request.Product.Code}' and p.is_deleted = 0")).FirstOrDefault();
                        if (product != null)
                        {
                            throw new BusinessException("Product.ExistedCode");
                        }

                        if (request.Product.ImageData?.Length > Constant.MaxImageLength)
                        {
                            throw new BusinessException("Image.OutOfLength");
                        }
                        //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
                        if (request.Product.ImageData?.Length > 200)
                        {
                            string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Product.ImageData));
                            if (!CommonHelper.IsImageType(type))
                            {
                                throw new BusinessException("Image.WrongType");
                            }
                            string Base64StringData = request.Product.ImageData.Substring(request.Product.ImageData.IndexOf(",") + 1);
                            string fileName = Guid.NewGuid().ToString().Replace("-", "");
                            request.Product.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.ProductImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
                        }	

                        request.Product.Code = await productQueries.GenarateCode();
                        request.Product = CreateBuild(request.Product, request.LoginSession);
                        var productId = await productRepository.Add(request.Product);

                        // languages
                        foreach (var item in request.Product.Languages)
                        {
                            item.ProductId = productId;
                            await productRepository.AddOrUpdateLanguage(item);
                        }

                        // prices
                        foreach (var item in request.Product.Prices)
                        {
                            item.ProductId = productId;
                            await productRepository.AddPrice(item);
                        }

                        rs = 0;
                    }
                    finally
                    {
                        if(rs == 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                            CommonHelper.DeleteImage(request.Product.ImageURL);
                        }
                    }
                }
            }

            return rs;
        }
    }
}
