using Common.Interfaces;
using MDM.UI.Languages.Models;
using System.Threading.Tasks;

namespace MDM.UI.Languages.Interfaces
{
    public interface ILanguageRepository: IBaseRepository
    {
        Task<int> AddAsync(Language language);
        Task<int> UpdateAsync(Language language);
        Task<int> DeleteAsync(int id);
    }
}
