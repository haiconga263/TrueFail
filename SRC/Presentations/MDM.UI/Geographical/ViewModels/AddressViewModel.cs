using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Geographical.ViewModels
{
    public class AddressViewModel : Address
    {
        public Country Country { set; get; }
        public Region Region { set; get; }
        public Province Province { set; get; }
        public District District { set; get; }
        public Ward Ward { set; get; }
    }
}
