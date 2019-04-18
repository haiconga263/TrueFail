using Common.Interfaces;
using MDM.UI.Categories.Models;
using System.Threading.Tasks;

namespace MDM.UI.Categories.Interfaces
{
    public interface ICategoryRepository: IBaseRepository
    {
        Task<int> AddAsync(Category category);
        Task<int> UpdateAsync(Category category);
        Task<int> DeleteAsync(int id);
    }
}
