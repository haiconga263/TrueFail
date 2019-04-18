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
    public class FarmerOrderRepository : BaseRepository, IFarmerOrderRepository
    {
        public async Task<int> Add(FarmerOrder order)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(order);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddItem(FarmerOrderItem item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(item);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteItems(long orderId)
        {
            var cmd = $"DELETE FROM `farmer_order_item` WHERE `farmer_order_id` = {orderId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(FarmerOrder order)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(order);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> UpdateItem(FarmerOrderItem item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(item);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0; ;
        }
    }
}
