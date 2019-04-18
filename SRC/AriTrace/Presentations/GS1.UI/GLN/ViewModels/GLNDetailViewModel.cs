using GS1.UI.GLN.Models;
using MDM.UI.Companies.Models;
using MDM.UI.Countries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS1.UI.GLN.ViewModels
{
	public class GLNDetailViewModel : GLNModel
	{
		public Company Company { set; get; }

		public Country Country { set; get; }
	}
}
