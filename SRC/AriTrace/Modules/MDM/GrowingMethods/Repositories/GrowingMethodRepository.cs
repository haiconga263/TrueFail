using Common.Models;
using DAL;
using MDM.UI.GrowingMethods.Interfaces;
using MDM.UI.GrowingMethods.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.GrowingMethods.Repositories
{
    public class GrowingMethodRepository : BaseRepository, IGrowingMethodRepository
    {
        public async Task<int> AddAsync(GrowingMethod growingMethod)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(growingMethod);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `growing_method` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(GrowingMethod growingMethod)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(growingMethod);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
