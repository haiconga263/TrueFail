using Common.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Geographical.Interfaces
{
    public interface ILocationRepository : IBaseRepository
    {
        Task<int> AddOrUpdateAddress(Address address);
        Task<int> AddOrUpdateContact(Contact contact);
    }
}
