using Homepage.UI.Models;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.PageCommand
{
	public class AddCommand : BaseCommand<int>
	{
		public PageHomepageViewModel Page { get; set; }
		public AddCommand(PageHomepageViewModel page)
		{
			this.Page = page;
		}
	}
}
