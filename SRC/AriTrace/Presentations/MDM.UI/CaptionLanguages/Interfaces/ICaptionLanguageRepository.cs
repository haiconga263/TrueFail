using Common.Interfaces;
using MDM.UI.CaptionLanguages.Models;
using System.Threading.Tasks;

namespace MDM.UI.CaptionLanguages.Interfaces
{
    public interface ICaptionLanguageRepository: IBaseRepository
    {
        Task<int> AddAsync(CaptionLanguage captionLanguage);
        Task<int> UpdateAsync(CaptionLanguage captionLanguage);
        Task<int> DeleteAsync(int id);
    }
}
