using MDM.UI.Farmers.Models;
using Order.UI.Models;
using System.Collections.Generic;

namespace Order.UI.ViewModels
{
    public class FarmerOrderViewModel : FarmerOrder
    {
        public Farmer Farmer { set; get; }
        public List<FarmerOrderItemViewModel> Items { set; get; } = new List<FarmerOrderItemViewModel>();
    }
}
