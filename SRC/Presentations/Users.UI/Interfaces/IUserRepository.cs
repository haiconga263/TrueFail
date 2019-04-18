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
    public interface IUserRepository : IBaseRepository
    {
        Task<UserSession> Login(UserAccountViewModel user, AccessTokenModel token);
        Task<UserSession> LoginAnotherUser(long sessionId, UserAccountViewModel user, AccessTokenModel token);
        Task Logout(UserSession userSession);
        Task<int> AddUser(UserAccount user, int? userRegister = null);
        Task<int> ChangePassword(int userId, string password);
        Task<int> ChangeEmail(int userId, string email);
        Task<int> RemoveUser(int userId);
        Task<int> UpdateUser(UserAccount user);
        Task<int> ActiveUser(int userId, string newPassword);
        Task<int> RegisterResetPassword(string userName, string pinCode);
        Task<int> ResetPassword(int userId, string password);
        Task<int> SaveChangeRoles(int userId, IEnumerable<int> roleIds);
        Task<int> InsertDevice(UserAccountDevice userAccountDevice);
    }
}
