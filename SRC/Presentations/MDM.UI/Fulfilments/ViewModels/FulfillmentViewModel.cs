using MDM.UI.Fulfillments.Models;
using MDM.UI.Geographical.Models;

namespace MDM.UI.Fulfillments.ViewModels
{
    public class FulfillmentViewModel : Fulfillment
    {
        public Address Address { set; get; }
        public Contact Contact { set; get; }

        public string ImageData { set; get; }
    }
}
