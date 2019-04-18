using System;
using System.Collections.Generic;
using System.Text;
using Homepage.UI.Models;

namespace Homepage.UI.ViewModels
{
	public class ContactHomepageViewModel : Contact
	{
		public string StatusStr => Status.ToString();
	}
}
