using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Products.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.ProductCommand
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IProductRepository productRepository = null;
        private readonly IProductQueries productQueries = null;
        public UpdateCommandHandler(IProductRepository productRepository, IProductQueries productQueries)
        {
            this.productRepository = productRepository;
            this.productQueries = productQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Product == null || request.Product.Id == 0)
            {
                throw new BusinessException("Product.NotExisted");
            }

            var product = (await productQueries.Get(request.Product.Id)).FirstOrDefault();
            if (product == null)
            {
                throw new BusinessException("Product.NotExisted");
            }

            if (request.Product.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }

            string oldImageUrl = request.Product.ImageURL;

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {

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

                        request.Product.CreatedDate = product.CreatedDate;
                        request.Product.CreatedBy = product.CreatedBy;
                        request.Product = UpdateBuild(request.Product, request.LoginSession);
                        request.Product.Code = product.Code;
                        rs = await productRepository.Update(request.Product);

                        if(rs != 0)
                        {
                            return -1;
                        }

                        //for language
                        // languages
                        foreach (var item in request.Product.Languages)
                        {
                            item.ProductId = request.Product.Id;
                            await productRepository.AddOrUpdateLanguage(item);
                        }

                        // prices
                        foreach (var item in request.Product.Prices)
                        {
                            item.ProductId = request.Product.Id;
                            if (item.Id != 0)
                            {
                                await productRepository.UpdatePrice(item);
                            }
                            else
                            {
                                await productRepository.AddPrice(item);
                            }
                        }

                        rs = 0;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                            if (request.Product.ImageData?.Length > 200)
                            {
                                LogHelper.GetLogger().Debug($"ImageData Length: {request.Product.ImageData.Length}. Deleted Image: {oldImageUrl}");
                                CommonHelper.DeleteImage(oldImageUrl);
                            }
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                            LogHelper.GetLogger().Debug($"Delete Image for process failed. Deleted Image: {request.Product.ImageURL}");
                            CommonHelper.DeleteImage(request.Product.ImageURL);
                        }
                    }
                }
            }

            return rs;
        }
    }
}
