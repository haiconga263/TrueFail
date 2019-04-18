using Common.Interfaces;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDM.UI.Products.Interfaces
{
    public interface IProductQueries : IBaseQueries
    {
        Task<string> GenarateCode();
        Task<IEnumerable<Product>> GetsOnly(); // VN
        Task<IEnumerable<ProductViewModel>> GetsOnlyWithLang(int languageId = 1); // VN

        Task<IEnumerable<ProductViewModel>> Get(int productId, int languageId = 1, DateTime? timeGet = null); // VN
        Task<IEnumerable<ProductViewModel>> Gets(string condition = "", int languageId = 1, DateTime? timeGet = null); // VN
        Task<IEnumerable<ProductViewModel>> GetsForOrder(string condition = "", int languageId = 1, DateTime? timeGet = null); // VN

        Task<IEnumerable<ProductViewModel>> GetsFull(string condition = "");
        Task<ProductViewModel> GetFull(int productId);
    }
}
