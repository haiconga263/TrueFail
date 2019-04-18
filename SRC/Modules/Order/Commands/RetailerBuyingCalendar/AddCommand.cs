using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.RetailerBuyingCalendar
{
    public class AddCommand : BaseCommand<int>
    {
        public RetailerBuyingCalendarViewModel BuyingCalendar { set; get; }
        public AddCommand(RetailerBuyingCalendarViewModel buyingCalendar)
        {
            BuyingCalendar = buyingCalendar;
        }
    }
}
