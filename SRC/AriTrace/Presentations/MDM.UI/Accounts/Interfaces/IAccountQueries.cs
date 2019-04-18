using Common.Interfaces;
using MDM.UI.Accounts.Models;
using MDM.UI.Accounts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Accounts.Interfaces
{
    public interface IAccountQueries : IBaseQueries
    {
        Task<AccountSingleRole> GetByIdAsync(int id);
        Task<IEnumerable<AccountSingleRole>> GetsAsync(int? partnerId = null);
        Task<IEnumerable<AccountSingleRole>> GetAllAsync(int? partnerId = null);
    }
}
