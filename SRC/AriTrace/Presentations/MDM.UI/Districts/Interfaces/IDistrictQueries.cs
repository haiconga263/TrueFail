using Common.Interfaces;
using MDM.UI.Districts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Districts.Interfaces
{
    public interface IDistrictQueries : IBaseQueries
    {
        Task<District> GetByIdAsync(int id);
        Task<IEnumerable<District>> GetsAsync();
        Task<IEnumerable<District>> GetAllAsync();
    }
}
