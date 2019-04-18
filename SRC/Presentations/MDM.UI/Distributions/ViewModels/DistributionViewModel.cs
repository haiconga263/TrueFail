using MDM.UI.Collections.Models;
using MDM.UI.Distributions.Models;
using MDM.UI.Geographical.Models;

namespace MDM.UI.Distributions.ViewModels
{
    public class DistributionViewModel : Distribution
    {
        public Address Address { set; get; }
        public Contact Contact { set; get; }

        public string ImageData { set; get; }
    }
}
