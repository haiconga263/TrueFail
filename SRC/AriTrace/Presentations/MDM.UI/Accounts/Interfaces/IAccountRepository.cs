using Common.Interfaces;
using MDM.UI.Accounts.Models;
using System.Threading.Tasks;

namespace MDM.UI.Accounts.Interfaces
{
    public interface IAccountRepository: IBaseRepository
    {
        Task<int> AddAsync(Account account);
        Task<int> UpdateAsync(Account account);
        Task<int> DeleteAsync(int id);
    }
}
