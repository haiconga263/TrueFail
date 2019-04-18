using Common.Interfaces;
using MDM.UI.CaptionLanguages.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.CaptionLanguages.Interfaces
{
    public interface ICaptionLanguageQueries : IBaseQueries
    {
        Task<CaptionLanguage> GetByIdAsync(int id);
        Task<IEnumerable<CaptionLanguage>> GetsAsync();
        Task<IEnumerable<CaptionLanguage>> GetAllAsync();
    }
}
