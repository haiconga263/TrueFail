using Common.Models;
using DAL;
using Dapper;
using MDM.UI.AccountRoles.Models;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.Mappings;
using MDM.UI.Accounts.Models;
using MDM.UI.Accounts.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Accounts.Queries
{
    public class AccountQueries : BaseQueries, IAccountQueries
    {
        public async Task<IEnumerable<AccountSingleRole>> GetAllAsync(int? partnerId = null)
        {
            string cmd = $@"SELECT u.*, u_r.* FROM `user_account` u 
                                LEFT JOIN `user_account_role` u_r
                                ON u_r.user_account_id = u.id AND u_r.role_id = (
                                    SELECT MIN(u_r_2.role_id)  role_id
                                    FROM `user_account_role` u_r_2
                                    WHERE u_r_2.user_account_id = u.id
                                    )
                                WHERE u.`is_deleted` = 0";

            if ((partnerId ?? 0) > 0)
                cmd += $" AND u.`partner_id`='{partnerId}'";
            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Account, AccountRole, AccountSingleRole>(
                           (accountRs, accountRoleRs) =>
                           {
                               AccountSingleRole account = null;
                               if (accountRs != null)
                               {
                                   account = accountRs.ToSingleRole();
                               }
                               else account = new AccountSingleRole();
                               if (accountRoleRs != null)
                               {
                                   account.RoleId = accountRoleRs.RoleId;
                               }
                               return account;
                           }
                       );
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<AccountSingleRole> GetByIdAsync(int id)
        {
            string cmd = $@"SELECT u.*, u_r.* FROM `user_account` u 
                                LEFT JOIN `user_account_role` u_r
                                ON u_r.user_account_id = u.id AND u_r.role_id = (
                                    SELECT MIN(u_r_2.role_id)  role_id
                                    FROM `user_account_role` u_r_2
                                    WHERE u_r_2.user_account_id = u.id
                                    )
                                WHERE u.`is_deleted` = 0 AND u.`id` = {id}";
            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Account, AccountRole, AccountSingleRole>(
                           (accountRs, accountRoleRs) =>
                           {
                               AccountSingleRole account = null;
                               if (accountRs != null)
                               {
                                   account = accountRs.ToSingleRole();
                               }
                               else account = new AccountSingleRole();
                               if (accountRoleRs != null)
                               {
                                   account.RoleId = accountRoleRs.RoleId;
                               }
                               return account;
                           }
                       ).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }

        }

        public async Task<IEnumerable<AccountSingleRole>> GetsAsync(int? partnerId = null)
        {
            string cmd = $@"SELECT u.*, u_r.* FROM `user_account` u 
                                LEFT JOIN `user_account_role` u_r
                                ON u_r.user_account_id = u.id AND u_r.role_id = (
                                    SELECT MIN(u_r_2.role_id)  role_id
                                    FROM `user_account_role` u_r_2
                                    WHERE u_r_2.user_account_id = u.id
                                    )
                                WHERE u.`is_used` = 1 AND u.`is_deleted` = 0";

            if ((partnerId ?? 0) > 0)
                cmd += $" AND u.`partner_id`='{partnerId}'";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Account, AccountRole, AccountSingleRole>(
                           (accountRs, accountRoleRs) =>
                           {
                               AccountSingleRole account = null;
                               if (accountRs != null)
                               {
                                   account = accountRs.ToSingleRole();
                               }
                               else account = new AccountSingleRole();
                               if (accountRoleRs != null)
                               {
                                   account.RoleId = accountRoleRs.RoleId;
                               }
                               return account;
                           }
                       );
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }

        }
    }
}
