using Common.Models;
using DAL;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Geographical.Repositories
{
    public class RegionRepository : BaseRepository, IRegionRepository
    {
        public async Task<int> Add(Region region)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(region);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Region region)
        {
            var cmd = $@"UPDATE `region`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {region.ModifiedBy},
                         `modified_date` = '{region.ModifiedDate.Value.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {region.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Region region)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(region);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
