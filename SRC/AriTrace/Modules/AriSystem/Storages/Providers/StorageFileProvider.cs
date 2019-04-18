using AriSystem.UI.Storages.Interfaces;
using AriSystem.UI.Storages.Models;
using GS1.UI.Productions.Models;
using MDM.UI.Companies.Models;
using MDM.UI.Products.Models;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AriSystem.Storages.Providers
{
    public class StorageFileProvider : IStorageFileProvider
    {
        private readonly ISettingQueries settingQueries;

        public StorageFileProvider(ISettingQueries settingQueries)
        {
            this.settingQueries = settingQueries;
        }

        public async Task<CompanyFolderPath> getCompanyFilePathAsync(Company company)
        {
            var root = $"{company?.Id ?? 0}";
            CompanyFolderPath filePath = new CompanyFolderPath()
            {
                RootPath = root,
                DefaultPath = $"{root}/default",
                LogoPath = $"{root}/logo",
                ProductionPath = $"{root}/production"
            };
            return filePath;
        }

        public async Task<ProductionFolderPath> getProductionFilePathAsync(Company company, Production production)
        {
            CompanyFolderPath companyFilePath = await getCompanyFilePathAsync(company);
            return getProductionFilePath(companyFilePath.ProductionPath, production);
        }

        public ProductionFolderPath getProductionFilePath(string productionPath, Production production)
        {
            var root = productionPath;
            var code = production?.GTINId.ToString() ?? "other";
            return new ProductionFolderPath()
            {
                RootPath = $"{root}",
                DefaultPath = $"{root}/{code}/default",
                ImageMarketingPath = $"{root}/{code}/marketing",
                ImageStandardPath = $"{root}/{code}/standard",
            };
        }

        public async Task<string> SaveCompanyLogo(CompanyFolderPath filePath, string type, string base64Data)
        {
            var path = await settingQueries.GetValueAsync(SettingKeys.Path_Company);

            string parentFolder = filePath.LogoPath;

            string fullPath = $"{path}/{parentFolder}";


            string filename = $"logo_{DateTime.Now.Ticks}.{type}";

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            File.WriteAllBytes($"{fullPath}/{filename}", Convert.FromBase64String(base64Data));
            return $"{parentFolder}/{filename}";
        }

        public async Task<ProductFolderPath> getProductFilePathAsync(Product product)
        {
            var root = $"{product?.Id ?? 0}";
            ProductFolderPath filePath = new ProductFolderPath()
            {
                RootPath = root,
                DefaultPath = $"{root}/default",
            };
            return filePath;
        }

        public async Task<string> SaveProductImage(ProductFolderPath filePath, string type, string base64Data)
        {
            var path = await settingQueries.GetValueAsync(SettingKeys.Path_Product);

            string parentFolder = filePath.DefaultPath;

            string fullPath = $"{path}/{parentFolder}";


            string filename = $"prod_{DateTime.Now.Ticks}.{type}";

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            File.WriteAllBytes($"{fullPath}/{filename}", Convert.FromBase64String(base64Data));
            return $"{parentFolder}/{filename}";
        }
    }
}
