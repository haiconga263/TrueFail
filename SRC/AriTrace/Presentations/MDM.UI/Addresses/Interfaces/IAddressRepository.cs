using Common.Interfaces;
using MDM.UI.Addresses.Models;
using System.Threading.Tasks;

namespace MDM.UI.Addresses.Interfaces
{
    public interface IAddressRepository: IBaseRepository
    {
        Task<int> AddAsync(Address address);
        Task<int> UpdateAsync(Address address);
        Task<int> DeleteAsync(int id);
    }
}
