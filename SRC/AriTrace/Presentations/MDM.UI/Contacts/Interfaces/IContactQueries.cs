using Common.Interfaces;
using MDM.UI.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Contacts.Interfaces
{
    public interface IContactQueries : IBaseQueries
    {
        Task<Contact> GetByIdAsync(int id);
        Task<IEnumerable<Contact>> GetsAsync();
        Task<IEnumerable<Contact>> GetAllAsync();
    }
}
