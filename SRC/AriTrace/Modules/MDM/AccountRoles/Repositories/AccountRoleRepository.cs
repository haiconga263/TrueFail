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

namespace MDM.AccountRoles.Repositories
{
    public class AccountRoleRepository : BaseRepository, IAccountRoleRepository
    {
        public async Task<int> AddAsync(AccountRole accountRole)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(accountRole);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `accountRole` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(AccountRole accountRole)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(accountRole);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
