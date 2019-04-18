using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using DAL;
using Users.UI.Interfaces;
using Users.UI.Models;
using Users.UI.ViewModels;

namespace Users.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<UserSession> Login(UserAccountViewModel user, AccessTokenModel token)
        {

            // Saved Session
            string cmd = $@"INSERT INTO `user_access_token`
                    (`user_id`,
                    `access_token`,
                    `login_date`,
                    `expired_date`)
                    VALUES
                    ({user.Id},
                    '{token.AccessToken}',
                    '{token.LoginDate.ToString("yyyy-MM-dd HH:mm-ss")}',
                    '{token.ExpiredDate.ToString("yyyy-MM-dd HH:mm-ss")}');
                    SELECT LAST_INSERT_ID();";
            var sessionId = (await DALHelper.ExecuteQuery<long>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();

            return new UserSession()
            {
                LoginResult = 0,
                SessionId = sessionId,
                UserName = user.UserName,
                Email = user.Email,
                IsSuperAdmin = user.IsSuperAdmin,
                IsUsed = user.IsUsed,
                Id = user.Id,
                Roles = user.Roles.Select(r => r.Name).ToList(),
                AccessToken = token.AccessToken
            };

        }

        public async Task<UserSession> LoginAnotherUser(long sessionId, UserAccountViewModel user, AccessTokenModel token)
        {

            // Saved Session again
            string cmd = $@"UPDATE `user_access_token`
                     SET
                     `user_id` = {user.Id},
                     `access_token` = '{token.AccessToken}',
                     `login_date` = '{token.LoginDate.ToString("yyyy-MM-dd HH:mm-ss")}',
                     `expired_date` = '{token.ExpiredDate.ToString("yyyy-MM-dd HH:mm-ss")}'
                     WHERE `id` = {sessionId};";
            await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);

            return new UserSession()
            {
                LoginResult = 0,
                SessionId = sessionId,
                UserName = user.UserName,
                Email = user.Email,
                IsSuperAdmin = user.IsSuperAdmin,
                IsUsed = user.IsUsed,
                Id = user.Id,
                Roles = user.Roles.Select(r => r.Name).ToList(),
                AccessToken = token.AccessToken
            };
        }

        public async Task Logout(UserSession userSession)
        {
            string cmd = $"DELETE FROM `user_access_token` WHERE `id` = {userSession.SessionId}";
            await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> AddUser(UserAccount user, int? userRegister = null)
        {
            string cmd = $@"INSERT INTO `user_account`
                            (`user_name`,
                            `password`,
                            `email`,
                            `security_password`,
                            `password_reset_code`,
                            `is_external_user`,
                            `is_actived`,
                            `created_by`)
                            VALUES
                            ('{user.UserName}',
                            '{user.Password}',
                            '{user.Email}',
                            '{user.SecurityPassword}',
                            NULL,
                            {(user.IsExternalUser ? 1 : 0)},
                            {(user.IsExternalUser ? 0 : 1)},
                            {(userRegister == null ? "NULL" : userRegister.Value.ToString())});
                     SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> ChangePassword(int userId, string password)
        {
            string cmd = $@"UPDATE `user_account`
                            SET
                            `password` = '{password}'
                            WHERE `id` = '{userId}';";
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> ChangeEmail(int userId, string email)
        {
            string cmd = $@"UPDATE `user_account`
                            SET
                            `email` = '{email}'
                            WHERE `id` = '{userId}';";
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> LockUser(int userId)
        {
            string cmd = $@"UPDATE `user_account`
                            SET
                            `is_used` = 0
                            WHERE `id` = '{userId}';";
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }
        public async Task<int> UpdateUser(UserAccount user)
        {
            string cmd = $@"UPDATE `user_account`
                            SET
                            `email` = '{user.Email}',
                            `is_used` = {(user.IsUsed ? "1" : "0")}
                            WHERE `id` = '{user.Id}';";
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> RegisterResetPassword(string userName, string pinCode)
        {
            string cmd = $@"UPDATE `user_account`
                            SET
                            `password_reset_code` = '{pinCode}'
                            WHERE `user_name` = '{userName}';";
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> RemoveUser(int userId)
        {
            string cmd = $@"UPDATE `user_account`
                            SET
                            `is_deleted` = 1
                            WHERE `id` = '{userId}';";
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> ResetPassword(int userId, string password)
        {
            string cmd = $@"UPDATE `user_account`
                            SET
                            `password` = '{password}',
                            `password_reset_code` = NULL
                            WHERE `id` = '{userId}';";
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> ActiveUser(int userId, string newPassword)
        {
            string cmd = $@"UPDATE `user_account`
                            SET
                            `is_actived` = 1,
                            `password` = '{newPassword}'
                            WHERE `id` = '{userId}';";
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> SaveChangeRoles(int userId, IEnumerable<int> roleIds)
        {
            int result = -1;
            if (DbConnection == null)
            {
                using (var conn = DALHelper.GetConnection())
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            //delete all role
                            await DALHelper.Execute($"DELETE FROM `user_account_role` WHERE `user_account_id` = {userId}", dbTransaction: trans, connection: conn);

                            foreach (var role in roleIds)
                            {
                                await DALHelper.Execute($@"INSERT INTO `user_account_role`
                                                       (`user_account_id`,
                                                       `role_id`)
                                                       VALUES
                                                       ({userId},
                                                       {role})", dbTransaction: trans, connection: conn);
                            }
                            trans.Commit();
                            result = 0;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                //delete all role
                await DALHelper.Execute($"DELETE FROM `user_account_role` WHERE `user_account_id` = {userId}", dbTransaction: DbTransaction, connection: DbConnection);

                foreach (var role in roleIds)
                {
                    await DALHelper.Execute($@"INSERT INTO `user_account_role`
                                                       (`user_account_id`,
                                                       `role_id`)
                                                       VALUES
                                                       ({userId},
                                                       {role})", dbTransaction: DbTransaction, connection: DbConnection);
                }
                result = 0;
            }

            return result;
        }

        public async Task<int> InsertDevice(UserAccountDevice userAccountDevice)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(userAccountDevice);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }
    }
}
