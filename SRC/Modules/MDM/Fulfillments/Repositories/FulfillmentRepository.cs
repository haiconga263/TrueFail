using Common.Models;
using DAL;
using MDM.UI.Collections.Models;
using MDM.UI.Fulfillments.Interfaces;
using MDM.UI.Fulfillments.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Fulfillments.Repositories
{
    public class FulfillmentRepository : BaseRepository, IFulfillmentRepository
    {
        public async Task<int> Add(Fulfillment fulfillment)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(fulfillment);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Fulfillment fulfillment)
        {
            var cmd = $@"UPDATE `fulfillment`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {fulfillment.ModifiedBy},
                         `modified_date` = '{fulfillment.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {fulfillment.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Fulfillment fulfillment)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(fulfillment);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
