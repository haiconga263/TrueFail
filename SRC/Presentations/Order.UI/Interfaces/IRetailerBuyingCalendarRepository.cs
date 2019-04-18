using Common.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IRetailerBuyingCalendarRepository : IBaseRepository
    {
        Task<int> Add(RetailerBuyingCalendar retailerBuyingCalendar);
        Task<int> Update(RetailerBuyingCalendar retailerBuyingCalendar);

        Task<int> AddItem(RetailerBuyingCalendarItem item);
        Task<int> UpdateItem(RetailerBuyingCalendarItem item);
        Task<int> DeleteItems(long buyingCalendarId);
    }
}
