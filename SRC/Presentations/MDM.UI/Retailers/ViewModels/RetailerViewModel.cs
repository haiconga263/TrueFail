using MDM.UI.Geographical.Models;
using MDM.UI.Geographical.ViewModels;
using MDM.UI.Retailers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Retailers.ViewModels
{
    public class RetailerViewModel : Retailer
    {
        public Address Address { set; get; }
        public Contact Contact { set; get; }

        public List<RetailerLocationViewModel> Locations { set; get; } = new List<RetailerLocationViewModel>();

        //mapping
        public string ImageData { set; get; }
    }
}
