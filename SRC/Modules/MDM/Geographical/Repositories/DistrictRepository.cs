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
    public class DistrictRepository : BaseRepository, IDistrictRepository
    {
        public async Task<int> Add(District district)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(district);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(District district)
        {
            var cmd = $@"UPDATE `district`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {district.ModifiedBy},
                         `modified_date` = '{district.ModifiedDate.Value.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {district.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(District district)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(district);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
