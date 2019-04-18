using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.FarmerBuyingCalendar
{
    public class ProcessInRetailerOrderCommand : BaseCommand<int>
    {
        public IEnumerable<FarmerBuyingCalendarViewModel> BuyingCalendars { set; get; }
        public ProcessInRetailerOrderCommand(IEnumerable<FarmerBuyingCalendarViewModel> buyingCalendars)
        {
            BuyingCalendars = buyingCalendars;
        }
    }
}
