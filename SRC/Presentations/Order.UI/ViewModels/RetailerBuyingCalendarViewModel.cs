using MDM.UI.Retailers.Models;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class RetailerBuyingCalendarViewModel : RetailerBuyingCalendar
    {
        public Retailer Retailer { set; get; }
        public List<RetailerBuyingCalendarItemViewModel> Items { set; get; } = new List<RetailerBuyingCalendarItemViewModel>();
    }
}
