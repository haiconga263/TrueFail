using Fulfillments.UI.Models;
using MDM.UI.Retailers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfillments.UI.ViewModels
{
    public class FulfillmentRetailerOrderViewModel : FulfillmentRetailerOrder
    {
        public Retailer Retailer { set; get; }
        public List<FulfillmentRetailerOrderItemViewModel> Items { set; get; } = new List<FulfillmentRetailerOrderItemViewModel>();    
    }
}
