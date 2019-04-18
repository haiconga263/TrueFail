using Common.Models;
using DAL;
using MDM.UI.AccountRoles.Interfaces;
using MDM.UI.AccountRoles.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.AccountRoles.Queries
{
    public class AccountRoleQueries : BaseQueries, IAccountRoleQueries
    {
        public async Task<IEnumerable<AccountRole>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `user_account_role`";
            return await DALHelper.Query<AccountRole>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<AccountRole> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<AccountRole>($"SELECT * FROM `user_account_role` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<AccountRole>> GetsAsync(string condition = "")
        {
            string cmd = $"SELECT * FROM `user_account_role`";
            if (!string.IsNullOrWhiteSpace(condition)) cmd += $" WHERE {condition} ";
            return await DALHelper.Query<AccountRole>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
