using Common.Interfaces;
using MDM.UI.Regions.Models;
using System.Threading.Tasks;

namespace MDM.UI.Regions.Interfaces
{
    public interface IRegionRepository: IBaseRepository
    {
        Task<int> AddAsync(Region region);
        Task<int> UpdateAsync(Region region);
        Task<int> DeleteAsync(int id);
    }
}
