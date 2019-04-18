using MDM.UI.Collections.ViewModels;
using MDM.UI.Fulfillments.ViewModels;
using Web.Controllers;

namespace Fulfillments.Commands
{
    public class AddCommand : BaseCommand<int>
    {
        public FulfillmentViewModel Fulfillment { set; get; }
        public AddCommand(FulfillmentViewModel fulfillment)
        {
            Fulfillment = fulfillment;
        }
    }
}
