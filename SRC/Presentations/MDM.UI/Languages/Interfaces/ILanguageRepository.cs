using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Languages.Interfaces
{
    public interface ILanguageRepository : IBaseRepository
    {
        Task<int> UpdateCaption(Caption caption);
        Task<int> DeleteCaptionLanguages(int captionId);
        Task<int> AddCaptionLanguage(CaptionLanguage language);
    }
}
