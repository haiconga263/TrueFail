using Common.Models;
using DAL;
using MDM.UI.Roles.Interfaces;
using MDM.UI.Roles.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Roles.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public async Task<int> AddAsync(Role role)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(role);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `role` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Role role)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(role);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
