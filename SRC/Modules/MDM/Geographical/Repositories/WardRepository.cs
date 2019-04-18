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
    public class WardRepository : BaseRepository, IWardRepository
    {
        public async Task<int> Add(Ward ward)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(ward);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Ward ward)
        {
            var cmd = $@"UPDATE `ward`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {ward.ModifiedBy},
                         `modified_date` = '{ward.ModifiedDate.Value.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {ward.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Ward ward)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(ward);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
