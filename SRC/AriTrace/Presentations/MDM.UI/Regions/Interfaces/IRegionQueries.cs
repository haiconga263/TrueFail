using Common.Interfaces;
using MDM.UI.Regions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Regions.Interfaces
{
    public interface IRegionQueries : IBaseQueries
    {
        Task<Region> GetByIdAsync(int id);

        Task<IEnumerable<Region>> GetsAsync();
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
