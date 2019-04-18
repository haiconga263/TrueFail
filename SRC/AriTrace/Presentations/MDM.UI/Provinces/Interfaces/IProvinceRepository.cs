using Common.Interfaces;
using MDM.UI.Provinces.Models;
using System.Threading.Tasks;

namespace MDM.UI.Provinces.Interfaces
{
    public interface IProvinceRepository: IBaseRepository
    {
        Task<int> AddAsync(Province province);
        Task<int> UpdateAsync(Province province);
        Task<int> DeleteAsync(int id);
    }
}
