using Common.Interfaces;
using MDM.UI.Addresses.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Addresses.Interfaces
{
    public interface IAddressQueries : IBaseQueries
    {
        Task<Address> GetByIdAsync(int id);
        Task<IEnumerable<Address>> GetsAsync();
        Task<IEnumerable<Address>> GetAllAsync();
    }
}
