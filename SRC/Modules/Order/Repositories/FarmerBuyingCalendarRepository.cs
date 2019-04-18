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
    public class FarmerBuyingCalendarRepository : BaseRepository, IFarmerBuyingCalendarRepository
    {
        public async Task<int> Add(FarmerBuyingCalendar farmerBuyingCalendar)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(farmerBuyingCalendar);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddItem(FarmerBuyingCalendarItem item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(item);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteItem(long itemId)
        {
            var cmd = $"DELETE FROM `farmer_buying_calendar_item` WHERE `id` = {itemId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> DeleteItems(long buyingCalendarId)
        {
            var cmd = $"DELETE FROM `farmer_buying_calendar_item` WHERE `farmer_buying_calendar_id` = {buyingCalendarId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(FarmerBuyingCalendar farmerBuyingCalendar)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(farmerBuyingCalendar);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> UpdateItem(FarmerBuyingCalendarItem item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(item);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
