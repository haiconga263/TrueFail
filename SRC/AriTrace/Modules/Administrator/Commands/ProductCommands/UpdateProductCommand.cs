using AriSystem.UI.Storages.Interfaces;
using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.ScheduleActions.Factories;
using MDM.UI.ProductLanguages.Interfaces;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using MDM.UI.ScheduleActions.Interfaces;
using MDM.UI.ScheduleActions.Models;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.ProductCommands
{
    public class UpdateProductCommand : BaseCommand<int>
    {
        public ProductMuiltipleLanguage Model { set; get; }
        public UpdateProductCommand(ProductMuiltipleLanguage product)
        {
            Model = product;
        }
    }

    public class UpdateProductCommandHandler : BaseCommandHandler<UpdateProductCommand, int>
    {
        private readonly IProductRepository productRepository = null;
        private readonly IProductQueries productQueries = null;

        private readonly IProductLanguageRepository productLanguageRepository = null;
        private readonly IProductLanguageQueries productLanguageQueries = null;

        private readonly IStorageFileProvider storageFile = null;
        private readonly IScheduleActionRepository scheduleActionRepository = null;

        private readonly ISettingQueries settingQueries = null;
        public UpdateProductCommandHandler(IProductRepository productRepository,
                                            IProductQueries productQueries,
                                            IProductLanguageRepository productLanguageRepository,
                                            IProductLanguageQueries productLanguageQueries,
                                            IStorageFileProvider storageFile,
                                            IScheduleActionRepository scheduleActionRepository,
                                            ISettingQueries settingQueries)
        {
            this.productRepository = productRepository;
            this.productQueries = productQueries;

            this.productLanguageRepository = productLanguageRepository;
            this.productLanguageQueries = productLanguageQueries;

            this.storageFile = storageFile;
            this.scheduleActionRepository = scheduleActionRepository;

            this.settingQueries = settingQueries;
        }
        public override async Task<int> HandleCommand(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Product.NotExisted");
            }
            else
            {
                product = await productQueries.GetByIdAsync(request.Model.Id);
                if (product == null)
                {
                    throw new BusinessException("Product.NotExisted");
                }
            }

            if ((request.Model.ImageData?.Length ?? 0) > Constant.MaxImageLength)
                throw new BusinessException("Image.OutOfLength");

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.CreatedBy = product.CreatedBy;
                        request.Model.CreatedDate = product.CreatedDate;

                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;

                        var filePath = await storageFile.getProductFilePathAsync(request.Model);

                        if (request.Model.IsChangedImage && (request.Model.ImageData?.Length ?? 0) > 0)
                        {
                            string oldLogoPath = product?.ImagePath ?? "";
                            string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Model.ImageData));
                            if (!CommonHelper.IsImageType(type))
                            {
                                throw new BusinessException("Image.WrongType");
                            }
                            string base64Data = request.Model.ImageData.Substring(request.Model.ImageData.IndexOf(",") + 1);
                            string fileName = Guid.NewGuid().ToString().Replace("-", "");
                            request.Model.ImagePath = await storageFile.SaveProductImage(filePath, type, base64Data);

                            if (string.IsNullOrWhiteSpace(oldLogoPath))
                            {
                                var imageFolderPath = settingQueries.GetValueAsync(SettingKeys.Path_Product);

                                var scheduleAction = (new RemoveFileAction() { FilePath = $"{imageFolderPath}/{oldLogoPath}" })
                                    .GetScheduleAction(request.LoginSession?.Id ?? 0);
                                await scheduleActionRepository.AddAsync(scheduleAction);
                            }
                        }

                        if (request.Model.ProductLanguages != null && request.Model.ProductLanguages.Count > 0)
                        {
                            foreach (var plang in request.Model.ProductLanguages)
                            {
                                if (plang.Id <= 0) await productLanguageRepository.AddAsync(plang);
                                else await productLanguageRepository.UpdateAsync(plang);
                            }
                        }

                        if (await productRepository.UpdateAsync(request.Model) > 0)
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
