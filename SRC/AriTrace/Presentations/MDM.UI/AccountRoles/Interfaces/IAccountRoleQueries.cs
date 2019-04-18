using Common.Interfaces;
using MDM.UI.AccountRoles.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.AccountRoles.Interfaces
{
    public interface IAccountRoleQueries : IBaseQueries
    {
        Task<AccountRole> GetByIdAsync(int id);
        Task<IEnumerable<AccountRole>> GetsAsync(string condition = "");
        Task<IEnumerable<AccountRole>> GetAllAsync();
    }
}
