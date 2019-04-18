using Common.Interfaces;
using MDM.UI.Roles.Models;
using System.Threading.Tasks;

namespace MDM.UI.Roles.Interfaces
{
    public interface IRoleRepository: IBaseRepository
    {
        Task<int> AddAsync(Role role);
        Task<int> UpdateAsync(Role role);
        Task<int> DeleteAsync(int id);
    }
}
