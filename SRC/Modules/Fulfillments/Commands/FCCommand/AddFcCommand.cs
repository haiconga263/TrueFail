using Fulfillments.UI.ViewModels;
using MDM.UI.Collections.ViewModels;
using MDM.UI.Fulfillments.ViewModels;
using Web.Controllers;

namespace Fulfillments.Commands.FCCommand
{
    public class AddFcCommand : BaseCommand<int>
    {
        public FulfillmentCollectionViewModel FulfillmentCollection { set; get; }
        public AddFcCommand(FulfillmentCollectionViewModel fulfillmentCollection)
        {
            this.FulfillmentCollection = fulfillmentCollection;
        }
    }
}
