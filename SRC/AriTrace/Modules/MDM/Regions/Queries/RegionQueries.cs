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

namespace MDM.Regions.Queries
{
    public class RegionQueries : BaseQueries, IRegionQueries
    {
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `region` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Region>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Region> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Region>($"SELECT * FROM `region` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Region>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `region` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Region>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
