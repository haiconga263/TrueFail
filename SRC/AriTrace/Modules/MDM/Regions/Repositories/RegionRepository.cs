using Common.Models;
using DAL;
using MDM.UI.Regions.Interfaces;
using MDM.UI.Regions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Regions.Repositories
{
    public class ProductionImageRepository : BaseRepository, IRegionRepository
    {
        public async Task<int> AddAsync(Region region)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(region);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `region` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Region region)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(region);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
