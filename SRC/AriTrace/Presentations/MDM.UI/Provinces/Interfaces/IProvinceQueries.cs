using Common.Interfaces;
using MDM.UI.Provinces.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Provinces.Interfaces
{
    public interface IProvinceQueries : IBaseQueries
    {
        Task<Province> GetByIdAsync(int id);

        Task<IEnumerable<Province>> GetsAsync();
        Task<IEnumerable<Province>> GetAllAsync();
    }
}
