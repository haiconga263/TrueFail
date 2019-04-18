using Fulfillments.UI.Models;
using MDM.UI.Products.Models;
using MDM.UI.UoMs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfillments.UI.ViewModels
{
    public class FulfillmentRetailerOrderItemViewModel : FulfillmentRetailerOrderItem
    {
        public Product Product { set; get; }
        public UoM UoM { set; get; }
    }

}
