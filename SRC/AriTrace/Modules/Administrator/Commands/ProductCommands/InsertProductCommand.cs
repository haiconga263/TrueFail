using AriSystem.UI.Storages.Interfaces;
using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.ProductLanguages.Interfaces;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using MDM.UI.ScheduleActions.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.ProductCommands
{
    public class InsertProductCommand : BaseCommand<int>
    {
        public ProductMuiltipleLanguage Model { set; get; }
        public InsertProductCommand(ProductMuiltipleLanguage product)
        {
            Model = product;
        }
    }

    public class InsertProductCommandHandler : BaseCommandHandler<InsertProductCommand, int>
    {
        private readonly IProductRepository productRepository = null;
        private readonly IProductQueries productQueries = null;

        private readonly IProductLanguageRepository productLanguageRepository = null;
        private readonly IProductLanguageQueries productLanguageQueries = null;

        private readonly IStorageFileProvider storageFile;
        private readonly IScheduleActionRepository scheduleActionRepository;
        public InsertProductCommandHandler(IProductRepository productRepository,
                                            IProductQueries productQueries,
                                            IProductLanguageRepository productLanguageRepository,
                                            IProductLanguageQueries productLanguageQueries,
                                            IStorageFileProvider storageFile,
                                            IScheduleActionRepository scheduleActionRepository)
        {
            this.productRepository = productRepository;
            this.productQueries = productQueries;

            this.productLanguageRepository = productLanguageRepository;
            this.productLanguageQueries = productLanguageQueries;

            this.storageFile = storageFile;
            this.scheduleActionRepository = scheduleActionRepository;
        }
        public override async Task<int> HandleCommand(InsertProductCommand request, CancellationToken cancellationToken)
        {
            var id = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession?.Id ?? 0;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession?.Id ?? 0;
                        id = await productRepository.AddAsync(request.Model);

                        Product product = await productQueries.GetByIdAsync(id);

                        if (request.Model.ProductLanguages != null && request.Model.ProductLanguages.Count > 0)
                        {
                            foreach (var plang in request.Model.ProductLanguages)
                            {
                                if (plang.Id <= 0)
                                {
                                    plang.ProductId = id;
                                    await productLanguageRepository.AddAsync(plang);
                                }
                                else await productLanguageRepository.UpdateAsync(plang);
                            }
                        }

                        var filePath = await storageFile.getProductFilePathAsync(product);

                        if ((request.Model.ImageData?.Length ?? 0) > Constant.MaxImageLength)
                            throw new BusinessException("Image.OutOfLength");

                        if (request.Model.IsChangedImage && (request.Model.ImageData?.Length ?? 0) > 0)
                        {
                            string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Model.ImageData));
                            if (!CommonHelper.IsImageType(type))
                            {
                                throw new BusinessException("Image.WrongType");
                            }
                            string base64Data = request.Model.ImageData.Substring(request.Model.ImageData.IndexOf(",") + 1);
                            string fileName = Guid.NewGuid().ToString().Replace("-", "");
                            product.ImagePath = await storageFile.SaveProductImage(filePath, type, base64Data);

                            await productRepository.UpdateAsync(product);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (id > 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return id;
        }
    }
}
