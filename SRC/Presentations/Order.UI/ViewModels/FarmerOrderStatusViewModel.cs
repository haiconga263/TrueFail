using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class FarmerOrderStatusViewModel : Order.UI.Models.FarmerOrderStatus
    {
        public string Name { set; get; }
        public string Description { set; get; }
    }
}
