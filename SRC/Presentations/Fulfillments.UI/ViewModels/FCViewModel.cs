using Fulfillments.UI.Models;
using MDM.UI.Collections.Models;
using MDM.UI.Fulfillments.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfillments.UI.ViewModels
{
    public class FCViewModel : FulfillmentCollection
    {
        public Fulfillment Fulfillment { get; set; }
        public Collection Collection { get; set; }
        public List<FCItemViewModel> Items { set; get; } = new List<FCItemViewModel>();

    }
}
