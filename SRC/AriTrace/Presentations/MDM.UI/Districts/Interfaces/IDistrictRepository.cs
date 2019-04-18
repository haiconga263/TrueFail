using Common.Interfaces;
using MDM.UI.Districts.Models;
using System.Threading.Tasks;

namespace MDM.UI.Districts.Interfaces
{
    public interface IDistrictRepository: IBaseRepository
    {
        Task<int> AddAsync(District district);
        Task<int> UpdateAsync(District district);
        Task<int> DeleteAsync(int id);
    }
}
