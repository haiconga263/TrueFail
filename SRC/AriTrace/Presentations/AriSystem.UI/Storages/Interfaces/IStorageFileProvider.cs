using AriSystem.UI.Storages.Models;
using GS1.UI.Productions.Models;
using MDM.UI.Companies.Models;
using MDM.UI.Products.Models;
using System.Threading.Tasks;

namespace AriSystem.UI.Storages.Interfaces
{
    public interface IStorageFileProvider
    {
        Task<CompanyFolderPath> getCompanyFilePathAsync(Company company);
        Task<ProductionFolderPath> getProductionFilePathAsync(Company company, Production production);
        ProductionFolderPath getProductionFilePath(string productionPath, Production production);
        Task<string> SaveCompanyLogo(CompanyFolderPath filePath, string type, string data);
        Task<ProductFolderPath> getProductFilePathAsync(Product model);
        Task<string> SaveProductImage(ProductFolderPath filePath, string type, string base64Data);
    }
}
