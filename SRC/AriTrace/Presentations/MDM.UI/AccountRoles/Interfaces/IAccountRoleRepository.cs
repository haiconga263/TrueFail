using Common.Interfaces;
using MDM.UI.AccountRoles.Models;
using System.Threading.Tasks;

namespace MDM.UI.AccountRoles.Interfaces
{
    public interface IAccountRoleRepository: IBaseRepository
    {
        Task<int> AddAsync(AccountRole accountRole);
        Task<int> UpdateAsync(AccountRole accountRole);
        Task<int> DeleteAsync(int id);
    }
}
