using Common.Interfaces;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Settings.Interfaces
{
    public interface ISettingQueries : IBaseQueries
    {
        Task<Setting> GetByIdAsync(int id);
        Task<IEnumerable<Setting>> GetsAsync();
        Task<IEnumerable<Setting>> GetAllAsync();

        Task<IEnumerable<Setting>> GetsByPrefixAsync(string prefix);
        Task<string> GetValueAsync(string key);
        Task<Setting> GetByKeyAsync(string key);
    }
}
