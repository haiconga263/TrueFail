using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.FarmerBuyingCalendar
{
    public class ProcessCommand : BaseCommand<int>
    {
        public FarmerBuyingCalendarViewModel BuyingCalendar { set; get; }
        public ProcessCommand(FarmerBuyingCalendarViewModel buyingCalendar)
        {
            BuyingCalendar = buyingCalendar;
        }
    }
}
