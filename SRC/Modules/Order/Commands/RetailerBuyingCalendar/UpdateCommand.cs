using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.RetailerBuyingCalendar
{
    public class UpdateCommand : BaseCommand<int>
    {
        public RetailerBuyingCalendarViewModel BuyingCalendar { set; get; }
        public UpdateCommand(RetailerBuyingCalendarViewModel buyingCalendar)
        {
            BuyingCalendar = buyingCalendar;
        }
    }
}
