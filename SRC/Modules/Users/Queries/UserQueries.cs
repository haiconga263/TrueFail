using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using Users.UI.Interfaces;
using Users.UI.Models;
using Users.UI.ViewModels;

namespace Users.Queries
{
    public class UserQueries : BaseQueries, IUserQueries
    {

        public async Task<Role> GetRole(string roleName)
        {
            return (await DALHelper.Query<Role>($"SELECT * FROM `role` WHERE `name` = '{roleName}'", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<Role>> GetRoles(int type = 2)
        {
            string cmd = $"SELECT * FROM `role`";
            if (type == 0)
            {
                cmd += " WHERE is_external_role = 0";
            }
            else if (type == 1)
            {
                cmd += " WHERE is_external_role = 1";
            }
            return await DALHelper.Query<Role>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<UserAccount> GetUser(string userName)
        {
            return (await DALHelper.Query<UserAccount>($"SELECT * FROM `user_account` WHERE `user_name` = '{userName}'", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<UserAccount> GetUser(int userId)
        {
            return (await DALHelper.Query<UserAccount>($"SELECT * FROM `user_account` WHERE `id` = '{userId}'", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<UserAccountViewModel> GetUserWithRole(string userName)
        {
            string cmd = $@"SELECT u.*, r.* FROM `user_account` u
                            LEFT JOIN `user_account_role` ur ON u.id = ur.user_account_id
                            LEFT JOIN `role` r ON ur.role_id = r.id and r.is_used = 1
                            WHERE u.`user_name` = '{userName}' and u.`is_deleted` = 0";
            UserAccountViewModel user = null;
            if (DbConnection == null)
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var reader = await conn.QueryMultipleAsync(cmd);
                    reader.Read<UserAccount, Role, UserAccountViewModel>(
                        (userRs, roleRs) =>
                        {
                            if (user == null)
                            {
                                user = CommonHelper.Mapper<UserAccount, UserAccountViewModel>(userRs);
                            }
                            if (roleRs != null)
                            {
                                user.Roles.Add(roleRs);
                            }
                            return user;
                        }
                    );
                    return user;
                }
            }
            else
            {
                var reader = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                reader.Read<UserAccount, Role, UserAccountViewModel>(
                    (userRs, roleRs) =>
                    {
                        if (user == null)
                        {
                            user = CommonHelper.Mapper<UserAccount, UserAccountViewModel>(userRs);
                        }
                        if (roleRs != null)
                        {
                            user.Roles.Add(roleRs);
                        }
                        return user;
                    }
                );
                return user;
            }
        }

        public async Task<UserAccountViewModel> GetUserWithRole(int userId)
        {
            string cmd = $@"SELECT u.*, r.* FROM `user_account` u
                            LEFT JOIN `user_account_role` ur ON u.id = ur.user_account_id
                            LEFT JOIN `role` r ON ur.role_id = r.id and r.is_used = 1
                            WHERE u.`id` = '{userId}' and u.`is_deleted` = 0";
            UserAccountViewModel user = null;
            if (DbConnection == null)
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var reader = await conn.QueryMultipleAsync(cmd);
                    reader.Read<UserAccount, Role, UserAccountViewModel>(
                        (userRs, roleRs) =>
                        {
                            if (user == null)
                            {
                                user = CommonHelper.Mapper<UserAccount, UserAccountViewModel>(userRs);
                            }
                            if (roleRs != null)
                            {
                                user.Roles.Add(roleRs);
                            }
                            return user;
                        }
                    ); return user;
                }
            }
            else
            {
                var reader = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                reader.Read<UserAccount, Role, UserAccountViewModel>(
                    (userRs, roleRs) =>
                    {
                        if (user == null)
                        {
                            user = CommonHelper.Mapper<UserAccount, UserAccountViewModel>(userRs);
                        }
                        if (roleRs != null)
                        {
                            user.Roles.Add(roleRs);
                        }
                        return user;
                    }
                ); return user;
            }
        }

        public async Task<IEnumerable<UserAccount>> GetUsers(string condition = "")
        {
            return await DALHelper.Query<UserAccount>($"SELECT * FROM `user_account` WHERE is_deleted = 0 and "
                                                      + (string.IsNullOrEmpty(condition) ? "1=1" : condition), dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<UserAccountViewModel>> GetUsersWithRole(string condition = "")
        {
            string cmd = $@"SELECT u.*, r.* FROM `user_account` u
                            LEFT JOIN `user_account_role` ur ON u.id = ur.user_account_id
                            LEFT JOIN `role` r ON ur.role_id = r.id and r.is_used = 1
                            WHERE u.`is_deleted` = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += $" AND {condition}";
            }
            List<UserAccountViewModel> users = new List<UserAccountViewModel>();
            if (DbConnection == null)
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var reader = await conn.QueryMultipleAsync(cmd);
                    reader.Read<UserAccount, Role, UserAccountViewModel>(
                        (userRs, roleRs) =>
                        {
                            var user = users.FirstOrDefault(u => u.Id == userRs.Id);
                            if (user == null)
                            {
                                user = CommonHelper.Mapper<UserAccount, UserAccountViewModel>(userRs);
                                users.Add(user);
                            }

                            if (roleRs != null)
                            {
                                var role = user.Roles.FirstOrDefault(r => r == roleRs);
                                if (role == null)
                                {
                                    user.Roles.Add(roleRs);
                                }
                            }
                            return user;
                        }
                    );
                    return users;
                }
            }
            else
            {
                var reader = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                reader.Read<UserAccount, Role, UserAccountViewModel>(
                    (userRs, roleRs) =>
                    {
                        var user = users.FirstOrDefault(u => u.Id == userRs.Id);
                        if (user == null)
                        {
                            user = CommonHelper.Mapper<UserAccount, UserAccountViewModel>(userRs);
                            users.Add(user);
                        }

                        if (roleRs != null)
                        {
                            var role = user.Roles.FirstOrDefault(r => r == roleRs);
                            if (role == null)
                            {
                                user.Roles.Add(roleRs);
                            }
                        }
                        return user;
                    }
                );
                return users;
            }
        }

        public async Task<IEnumerable<UserAccount>> GetUsersNotAssignBy(bool isExternalUser, string roleName = "")
        {
            string cmd = $@"SELECT u.id, u.user_name FROM `user_account` u
                            WHERE u.`is_deleted` = 0
                                  AND u.`is_external_user` = {(isExternalUser ? "1" : "0")}
                                  AND u.is_superadmin = 0
                                  AND u.is_actived = 1
                                  AND NOT EXISTS (SELECT e.id FROM `employee` e
                                                  WHERE e.user_account_id = u.id)
                                  AND NOT EXISTS (SELECT e.id FROM `farmer` e
                                                  WHERE e.user_account_id = u.id)
                                  AND NOT EXISTS (SELECT e.id FROM `retailer` e
                                                  WHERE e.user_account_id = u.id)";
            if (!string.IsNullOrEmpty(roleName))
            {
                cmd += $@" AND EXISTS (SELECT ar.id FROM `user_account_role` ar 
                                       JOIN `role` r ON ar.role_id = r.id AND r.is_used = 1 AND r.name = '{roleName}'
                                       WHERE ar.user_account_id = u.id)";
            }
            return await DALHelper.Query<UserAccount>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<UserAccountDevice>> GetDevicesByUserIdAsync(int userId)
        {
            return await DALHelper.Query<UserAccountDevice>($"SELECT * FROM `user_account_device` WHERE `user_id` = '{userId}'", dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<UserAccount> GetUserWithEmail(string email)
        {
            return (await DALHelper.Query<UserAccount>($"SELECT * FROM `user_account` WHERE `email` = '{email}'", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }
    }
}
