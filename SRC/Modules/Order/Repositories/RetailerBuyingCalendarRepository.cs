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
    public class RetailerBuyingCalendarRepository : BaseRepository, IRetailerBuyingCalendarRepository
    {
        public async Task<int> Add(RetailerBuyingCalendar retailerBuyingCalendar)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(retailerBuyingCalendar);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddItem(RetailerBuyingCalendarItem item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(item);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteItems(long buyingCalendarId)
        {
            var cmd = $"DELETE FROM `retailer_buying_calendar_item` WHERE `retailer_buying_calendar_id` = {buyingCalendarId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(RetailerBuyingCalendar retailerBuyingCalendar)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(retailerBuyingCalendar);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> UpdateItem(RetailerBuyingCalendarItem item)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(item);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
