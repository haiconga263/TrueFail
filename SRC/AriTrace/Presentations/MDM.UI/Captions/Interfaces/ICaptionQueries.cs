using Common.Interfaces;
using MDM.UI.Captions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MDM.UI.Captions.ViewModels;

namespace MDM.UI.Captions.Interfaces
{
    public interface ICaptionQueries : IBaseQueries
    {
        Task<CaptionMultipleLanguage> GetByIdAsync(int id);
        Task<IEnumerable<Caption>> GetsAsync();
        Task<IEnumerable<Caption>> GetAllAsync();
    }
}
