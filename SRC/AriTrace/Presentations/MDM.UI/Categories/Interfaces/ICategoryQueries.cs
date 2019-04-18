using Common.Interfaces;
using MDM.UI.Categories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Categories.Interfaces
{
    public interface ICategoryQueries : IBaseQueries
    {
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetsAsync();
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
