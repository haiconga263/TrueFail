using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.PageCommand
{
	public class UpdateCommand : BaseCommand<int>
	{
		public PageHomepageViewModel Page { set; get; }
		public UpdateCommand(PageHomepageViewModel page)
		{
			this.Page = page;
		}
	}
}
