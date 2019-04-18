using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.RetailerBuyingCalendar
{
    public class DeleteCommand : BaseCommand<int>
    {
        public long BuyingCalendarId { set; get; }
        public DeleteCommand(long buyingCalendarId)
        {
            BuyingCalendarId = buyingCalendarId;
        }
    }
}
