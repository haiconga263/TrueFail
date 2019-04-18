using Common.Interfaces;
using MDM.UI.Contacts.Models;
using System.Threading.Tasks;

namespace MDM.UI.Contacts.Interfaces
{
    public interface IContactRepository: IBaseRepository
    {
        Task<int> AddAsync(Contact contact);
        Task<int> UpdateAsync(Contact contact);
        Task<int> DeleteAsync(int id);
    }
}
