using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class ComfirmFarmerBuyingCalendarViewModel
    {

        public long Id { get; set; }
        public long FarmerBuyingCalendarId { get; set; }
        public int AdapQuantity { get; set; }
        public string AdapNote { get; set; }
    }
}
