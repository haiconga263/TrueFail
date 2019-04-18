using Common.Interfaces;
using MDM.UI.Wards.Models;
using System.Threading.Tasks;

namespace MDM.UI.Wards.Interfaces
{
    public interface IWardRepository: IBaseRepository
    {
        Task<int> AddAsync(Ward ward);
        Task<int> UpdateAsync(Ward ward);
        Task<int> DeleteAsync(int id);
    }
}
