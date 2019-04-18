using Distributions.UI.Models;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distributions.UI.ViewModels
{
    public class RouterViewModel : Router
    {
        public Country Country { set; get; }
        public Province Province { set; get; }
    }
}
