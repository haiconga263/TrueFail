using Common.Interfaces;
using MDM.UI.Categories.Models;
using System.Threading.Tasks;

namespace MDM.UI.Categories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository
    {
        Task<int> Add(Category category);
        Task<int> Update(Category category);
        Task<int> Delete(int id);
        Task<int> AddOrUpdateLanguage(CategoryLanguage language);
    }
}
