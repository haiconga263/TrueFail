using System;
using System.Linq;
using System.Threading.Tasks;
using Collections.UI.PurchaseOrders.Interfaces;
using Collections.UI.PurchaseOrders.Models;
using Common.Models;
using DAL;

namespace Collections.PurchaseOrders.Repositories
{
    public class PurchaseOrderRepository : BaseRepository, IPurchaseOrderRepository
    {
        public async Task<int> AddAsync(PurchaseOrder po)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(po);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<bool> DeleteAsync(int id, int modifiedBy)
        {
            var cmd = $@"UPDATE `collection_purchase_order`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {modifiedBy},
                         `modified_date` = '{DateTime.Now.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs != 0;
        }

        public async Task<bool> UpdateAsync(PurchaseOrder po)
        {
            string cmd = QueriesCreatingHelper.CreateQueryUpdate(po);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs != 0;
        }

        public async Task<int> AddItemAsync(PurchaseOrderItem poItem)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(poItem);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<bool> UpdateItemAsync(PurchaseOrderItem poItem)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(poItem);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs != 0;
        }
    }
}
