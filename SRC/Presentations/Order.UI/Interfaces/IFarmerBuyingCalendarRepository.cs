using Common.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IFarmerBuyingCalendarRepository : IBaseRepository
    {
        Task<int> Add(FarmerBuyingCalendar farmerBuyingCalendar);
        Task<int> Update(FarmerBuyingCalendar farmerBuyingCalendar);

        Task<int> AddItem(FarmerBuyingCalendarItem item);
        Task<int> UpdateItem(FarmerBuyingCalendarItem item);
        Task<int> DeleteItems(long buyingCalendarId);
        Task<int> DeleteItem(long itemId);
    }
}
