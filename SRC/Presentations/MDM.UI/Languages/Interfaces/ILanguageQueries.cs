using Common.Interfaces;
using Common.Models;
using Common.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDM.UI.Languages.Interfaces
{
    public interface ILanguageQueries : IBaseQueries
    {
        Task<IEnumerable<Language>> Gets(string condition = "");
        Task<IEnumerable<CaptionViewModel>> GetCaptions();
        Task<CaptionViewModel> GetCaption(int captionId);
    }
}
