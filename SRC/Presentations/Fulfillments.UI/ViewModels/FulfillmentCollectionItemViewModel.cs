using Fulfillments.UI.Models;
using MDM.UI.Common.Models;
using MDM.UI.Products.Models;
using MDM.UI.UoMs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfillments.UI.ViewModels
{
    public class FulfillmentCollectionItemViewModel : FulfillmentCollectionItem
    {
        public Product Product { get; set; }
        public UoM UoM { get; set; }
    }
}
