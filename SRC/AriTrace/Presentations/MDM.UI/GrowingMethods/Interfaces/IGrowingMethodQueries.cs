using Common.Interfaces;
using MDM.UI.GrowingMethods.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.GrowingMethods.Interfaces
{
    public interface IGrowingMethodQueries : IBaseQueries
    {
        Task<GrowingMethod> GetByIdAsync(int id);
        Task<IEnumerable<GrowingMethod>> GetsAsync();
        Task<IEnumerable<GrowingMethod>> GetAllAsync();
    }
}
