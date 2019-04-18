using Common.Models;
using DAL;
using Production.UI.MaterialHistories.Interfaces;
using Production.UI.MaterialHistories.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.MaterialHistories.Repositories
{
    public class MaterialHistoryRepository : BaseRepository, IMaterialHistoryRepository
    {
        public async Task<int> AddAsync(MaterialHistory materialHistory)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(materialHistory);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `material_history` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(MaterialHistory materialHistory)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(materialHistory);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
