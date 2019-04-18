using Common.Models;
using DAL;
using Order.UI.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repositories
{
    public class RetailerOrderRepository : BaseRepository, IRetailerOrderRepository
    {
        public async Task<int> Add(RetailerOrder order)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(order);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddAudit(RetailerOrderAudit audit)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(audit);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddItem(RetailerOrderItem item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(item);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteItems(long orderId)
        {
            var cmd = $"DELETE FROM `retailer_order_item` WHERE `retailer_order_id` = {orderId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(RetailerOrder order)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(order);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> UpdateItem(RetailerOrderItem item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(item);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0; ;
        }
    }
}
