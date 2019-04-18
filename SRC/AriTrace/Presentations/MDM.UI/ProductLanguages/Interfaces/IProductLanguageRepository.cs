using Common.Interfaces;
using MDM.UI.ProductLanguages.Models;
using System.Threading.Tasks;

namespace MDM.UI.ProductLanguages.Interfaces
{
    public interface IProductLanguageRepository: IBaseRepository
    {
        Task<int> AddAsync(ProductLanguage productLanguage);
        Task<int> UpdateAsync(ProductLanguage productLanguage);
        Task<int> DeleteAsync(int id);
    }
}
