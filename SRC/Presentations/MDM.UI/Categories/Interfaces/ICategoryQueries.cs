using Common.Interfaces;
using MDM.UI.Categories.Models;
using MDM.UI.Categories.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDM.UI.Categories.Interfaces
{
    public interface ICategoryQueries : IBaseQueries
    {
        Task<CategoryLanguageViewModel> GetById(int id);
        Task<IEnumerable<Category>> Gets(string lang = "vi");
        Task<IEnumerable<Category>> GetAll(string lang = "vi");

        Task<IEnumerable<CategoryViewModel>> GetsWithChild(string lang = "vi");
    }
}
