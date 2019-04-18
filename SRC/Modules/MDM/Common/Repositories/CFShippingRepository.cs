using Common.Models;
using DAL;
using MDM.UI.Common.Interfaces;
using MDM.UI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Common.Repositories
{
    public class CFShippingRepository : BaseRepository, ICFShippingRepository
    {
        public async Task<int> Add(CFShipping shipping)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(shipping);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddItem(CFShippingItem shippingItem)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(shippingItem);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(CFShipping shipping)
        {
            var cmd = $@"UPDATE `cf_shipping`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {shipping.ModifiedBy},
                         `modified_date` = '{shipping.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {shipping.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> DeleteItem(long cfShippingItemId)
        {
            var cmd = $@"DELETE FROM `cf_shipping_item` WHERE `id` = {cfShippingItemId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> DeleteItems(long cfShippingId)
        {
            var cmd = $@"DELETE FROM `cf_shipping_item` WHERE `cf_shipping_id` = {cfShippingId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(CFShipping shipping)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(shipping);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> UpdateItem(CFShippingItem shippingItem)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(shippingItem);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
