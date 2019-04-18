using Homepage.UI.Models;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.FaqCommand
{
	public class AddCommand : BaseCommand<int>
	{
		public FaqHomepageViewModel Faq { get; set; }
		public AddCommand(FaqHomepageViewModel faq)
		{
			this.Faq = faq;
		}
	}
}
