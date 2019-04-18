using MDM.UI.Products.Models;
using MDM.UI.UoMs.Models;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class FarmerOrderItemViewModel : FarmerOrderItem
    {
        public Product Product { set; get; }
        public UoM UoM { set; get; }
    }
}
