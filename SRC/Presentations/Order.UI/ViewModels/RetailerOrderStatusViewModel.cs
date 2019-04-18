using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class RetailerOrderStatusViewModel : Order.UI.Models.RetailerOrderStatus
    {
        public string Name { set; get; }
        public string Description { set; get; }
    }
}
