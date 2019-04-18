using Common.Models;
using DAL;
using MDM.UI.Farmers.Interfaces;
using MDM.UI.Farmers.Models;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Farmers.Repositories
{
    public class FarmerRepository : BaseRepository, IFarmerRepository
    {
        public async Task<int> Add(Farmer farmer)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(farmer);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First(); 
        }

        public async Task<int> Delete(Farmer farmer)
        {
            var cmd = $@"UPDATE `farmer`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {farmer.ModifiedBy},
                         `modified_date` = '{farmer.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {farmer.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Farmer farmer)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(farmer);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
