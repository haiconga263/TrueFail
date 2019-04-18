using Common.Interfaces;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Products.Interfaces
{
    public interface IProductQueries : IBaseQueries
    {
        Task<ProductMuiltipleLanguage> GetByIdAsync(int id);

        Task<IEnumerable<Product>> GetsAsync();
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
