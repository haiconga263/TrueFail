using Common.Models;
using DAL;
using GS1.UI.Productions.Interfaces;
using GS1.UI.Productions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1.Productions.Repositories
{
    public class ProductionRepository : BaseRepository, IProductionRepository
    {
        public async Task<int> AddAsync(Production production)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(production);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `production` WHERE `id` = {id}; delete from `production_image` WHERE `production_id` = {id}; ";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Production production)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(production);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
