using System;
using System.Collections.Generic;
using System.Text;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.Models;
using MDM.UI.Retailers.ViewModels;

namespace Homepage.UI.ViewModels
{
	public class RetailerHomepageViewModel : Retailer
	{
		public Address Address { set; get; }
		public Contact Contact { set; get; }
		//public List<RetailerLocationViewModel> Locations { set; get; } = new List<RetailerLocationViewModel>();
	}
}
