using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homepage.UI.ViewModels
{
	public class PageHomepageViewModel : Page
	{
		public List<PageLanguage> PageLanguages { get; set; } = new List<PageLanguage>();
		public string TitleDisplay { get; set; }
		public string ContentDisplay { get; set; }
	}
}
