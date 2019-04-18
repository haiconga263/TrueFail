using Common.Interfaces;
using MDM.UI.GrowingMethods.Models;
using System.Threading.Tasks;

namespace MDM.UI.GrowingMethods.Interfaces
{
    public interface IGrowingMethodRepository: IBaseRepository
    {
        Task<int> AddAsync(GrowingMethod growingMethod);
        Task<int> UpdateAsync(GrowingMethod growingMethod);
        Task<int> DeleteAsync(int id);
    }
}
