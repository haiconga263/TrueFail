using Fulfillments.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Fulfillments.Commands.FCCommand
{
    public class FulfillmentCollectionCommand : BaseCommand<int>
    {
        public FulfillmentCollectionViewModel Fulfillment { set; get; }
        public FulfillmentCollectionCommand(FulfillmentCollectionViewModel fulfillment)
        {
            Fulfillment = fulfillment;
        }
    }
}
