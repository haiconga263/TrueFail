using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.UI.Models;
using Users.UI.ViewModels;

namespace Users.UI.Interfaces
{
    public interface IUserQueries : IBaseQueries
    {
        Task<IEnumerable<UserAccount>> GetUsers(string condition = "");
        Task<IEnumerable<UserAccountViewModel>> GetUsersWithRole(string condition = "");
        Task<IEnumerable<UserAccount>> GetUsersNotAssignBy(bool isExternalUser, string roleName = "");
        Task<UserAccount> GetUser(string userName);
        Task<UserAccount> GetUserWithEmail(string email);
        Task<UserAccount> GetUser(int userId);
        Task<UserAccountViewModel> GetUserWithRole(string userName);
        Task<UserAccountViewModel> GetUserWithRole(int userId);
        Task<IEnumerable<Role>> GetRoles(int type = 2);
        Task<Role> GetRole(string roleName);
        Task<IEnumerable<UserAccountDevice>> GetDevicesByUserIdAsync(int userId);
    }
}
