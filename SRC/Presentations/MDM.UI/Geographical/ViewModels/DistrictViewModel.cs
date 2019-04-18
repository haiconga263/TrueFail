using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Geographical.ViewModels
{
    public class DistrictViewModel : District
    {
        public Country Country { set; get; }
        public Region Region { set; get; }
        public Province Province { set; get; }
    }
}
