using Common.Interfaces;
using MDM.UI.Roles.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Roles.Interfaces
{
    public interface IRoleQueries : IBaseQueries
    {
        Task<Role> GetByIdAsync(int id);
        Task<IEnumerable<Role>> GetsAsync();
        Task<IEnumerable<Role>> GetAllAsync();
    }
}
