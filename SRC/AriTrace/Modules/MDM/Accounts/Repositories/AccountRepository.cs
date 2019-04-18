using Common.Models;
using DAL;
using MDM.UI.Accounts.Interfaces;
using MDM.UI.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Accounts.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public async Task<int> AddAsync(Account account)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(account);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `user_account` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Account account)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(account);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
