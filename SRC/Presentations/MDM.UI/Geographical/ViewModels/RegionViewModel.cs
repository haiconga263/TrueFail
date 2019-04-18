using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Geographical.ViewModels
{
    public class RegionViewModel : Region
    {
        public Country Country { set; get; }
    }
}
