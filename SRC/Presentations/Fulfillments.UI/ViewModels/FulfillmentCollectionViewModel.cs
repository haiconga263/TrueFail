using Fulfillments.UI.Models;
using MDM.UI.Collections.Models;
using MDM.UI.Common.Models;
using MDM.UI.Fulfillments.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfillments.UI.ViewModels
{
    public class FulfillmentCollectionViewModel : FulfillmentCollection
    {
        public Fulfillment Fulfillment { get; set; }
        public Collection Collection { get; set; }
        public List<FulfillmentCollectionItemViewModel> Items { set; get; } = new List<FulfillmentCollectionItemViewModel>();
		public List<StatusViewModel> StatusFulCols { get; set; }
	}
}
