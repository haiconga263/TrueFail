using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Retailers.ViewModels
{
    public class RetailerLocationViewModel : RetailerLocation
    {
        public Address Address { set; get; }
        public Contact Contact { set; get; }


        //mapping
        public string ImageData { set; get; }
    }
}
