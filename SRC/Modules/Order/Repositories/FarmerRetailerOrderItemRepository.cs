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
    public class FarmerRetailerOrderItemRepository : BaseRepository, IFarmerRetailerOrderItemRepository
    {
        public async Task<int> Add(FarmerRetailerOrderItems item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(item);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(long itemId)
        {
            return (await DALHelper.Execute($"DELETE FROM `farmer_retailer_order_items` WHERE `id` = {itemId}", dbTransaction: DbTransaction, connection: DbConnection)) == 0 ? -1 : 0;
        }

        public async Task<int> Delete(string condition)
        {
            string cmd = "DELETE FROM `farmer_retailer_order_items`";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += " WHERE " + condition;
            }
            return (await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection)) == 0 ? -1 : 0;
        }

        public async Task<int> Update(FarmerRetailerOrderItems item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(item);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
