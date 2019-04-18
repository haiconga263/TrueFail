using MDM.UI.Fulfillments.ViewModels;
using Web.Controllers;

namespace Fulfillments.Commands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public FulfillmentViewModel Fulfillment { set; get; }
        public UpdateCommand(FulfillmentViewModel fulfillment)
        {
            Fulfillment = fulfillment;
        }
    }
}
