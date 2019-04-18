using Common.Models;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Distributions.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Distributions.Repositories
{
    public class DistributionRepository : BaseRepository, IDistributionRepository
    {
        public async Task<int> Add(Distribution distribution)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(distribution);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Distribution distribution)
        {
            var cmd = $@"UPDATE `distribution`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {distribution.ModifiedBy},
                         `modified_date` = '{distribution.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {distribution.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Distribution distribution)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(distribution);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
