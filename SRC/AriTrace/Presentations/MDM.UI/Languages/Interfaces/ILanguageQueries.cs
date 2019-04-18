using Common.Interfaces;
using MDM.UI.Languages.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Languages.Interfaces
{
    public interface ILanguageQueries : IBaseQueries
    {
        Task<Language> GetByIdAsync(int id);
        Task<IEnumerable<Language>> GetsAsync();
        Task<IEnumerable<Language>> GetAllAsync();
    }
}
