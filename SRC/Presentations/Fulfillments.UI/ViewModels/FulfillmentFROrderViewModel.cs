using Fulfillments.UI.Models;
using MDM.UI.Retailers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfillments.UI.ViewModels
{
    public class FulfillmentFROrderViewModel : FulfillmentPack
    {
        public FulfillmentTeam Team { get; set; }
        public List<FulfillmentFROrderItemViewModel> Items { set; get; } = new List<FulfillmentFROrderItemViewModel>();
    }
}
