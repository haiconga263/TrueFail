using MDM.UI.Farmers.Models;
using Order.UI.Models;
using System.Collections.Generic;

namespace Order.UI.ViewModels
{
    public class FarmerBuyingCalendarViewModel : FarmerBuyingCalendar
    {
        public Farmer Farmer { set; get; }
        public List<FarmerBuyingCalendarItemViewModel> Items { set; get; } = new List<FarmerBuyingCalendarItemViewModel>();

        //Mapping property
        public int RetailerId { set; get; }
    }
}
