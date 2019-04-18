using Common.Interfaces;
using MDM.UI.Countries.Models;
using System.Threading.Tasks;

namespace MDM.UI.Countries.Interfaces
{
    public interface ICountryRepository: IBaseRepository
    {
        Task<int> AddAsync(Country country);
        Task<int> UpdateAsync(Country country);
        Task<int> DeleteAsync(int id);
    }
}
