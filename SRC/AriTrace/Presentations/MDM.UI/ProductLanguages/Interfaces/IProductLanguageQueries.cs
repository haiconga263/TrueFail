using Common.Interfaces;
using MDM.UI.ProductLanguages.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.ProductLanguages.Interfaces
{
    public interface IProductLanguageQueries : IBaseQueries
    {
        Task<ProductLanguage> GetByIdAsync(int id);
        Task<IEnumerable<ProductLanguage>> GetsAsync();
        Task<IEnumerable<ProductLanguage>> GetAllAsync();
    }
}
