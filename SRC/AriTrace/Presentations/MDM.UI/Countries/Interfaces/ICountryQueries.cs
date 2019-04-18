using Common.Interfaces;
using MDM.UI.Countries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Countries.Interfaces
{
    public interface ICountryQueries : IBaseQueries
    {
        Task<Country> GetByIdAsync(int id);
        Task<IEnumerable<Country>> GetsAsync();
        Task<IEnumerable<Country>> GetAllAsync();
    }
}
