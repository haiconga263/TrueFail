using Common.Interfaces;
using MDM.UI.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Products.Interfaces
{
    public interface IProductRepository : IBaseRepository
    {
        Task<int> Add(Product product);
        Task<int> Update(Product product);
        Task<int> Delete(Product product);


        Task<int> AddOrUpdateLanguage(ProductLanguage language);
        Task<int> AddPrice(ProductPrice price);
        Task<int> UpdatePrice(ProductPrice price);
    }
}
