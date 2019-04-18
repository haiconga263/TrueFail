using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Geographical.ViewModels
{
    public class ProvinceViewModel : Province
    {
        public Country Country { set; get; }
        public Region Region { set; get; }
    }
}
