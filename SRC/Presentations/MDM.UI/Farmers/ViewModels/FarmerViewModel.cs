using MDM.UI.Farmers.Models;
using MDM.UI.Geographical.Models;
using MDM.UI.Geographical.ViewModels;

namespace MDM.UI.Farmers.ViewModels
{
    public class FarmerViewModel : Farmer
    {
        public Address Address { set; get; }
        public Contact Contact { set; get; }

        //Mapping
        public string ImageData { set; get; }
    }
}
