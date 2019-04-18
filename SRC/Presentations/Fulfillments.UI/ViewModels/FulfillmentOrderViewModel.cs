using Fulfillments.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfillments.UI.ViewModels
{
    public class FulfillmentOrderViewModel : Order.UI.ViewModels.RetailerOrderViewModel
    {
        public string Status { get; set; }
        public FulfillmentTeam Team { get; set; }
    }
}
