using Common.Interfaces;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
    public interface IProductHomepageQueries : IBaseQueries
    {
        Task<IEnumerable<ProductHomepageViewModel>> GetProductAsync(string condition = "", int pageIndex = 2, int pageSize = 10, string lang = "vi");
        Task<ProductHomepageViewModel> GetProductDetailOfHomepageAsync(int productId, string lang = "vi");
        Task<IEnumerable<ProductHomepageViewModel>> GetProductRelatedAsync(int categoryId, string lang = "vi");
        Task<IEnumerable<ProductHomepageViewModel>> GetProductOutstandingOfHomepage(string lang = "vi");
        Task<IEnumerable<ProductHomepageViewModel>> GetProductByCategory(int categoryId, string lang = "vi");
    }
}
