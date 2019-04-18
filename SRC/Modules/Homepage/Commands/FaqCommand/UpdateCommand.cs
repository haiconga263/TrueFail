using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.FaqCommand
{
	public class UpdateCommand : BaseCommand<int>
	{
		public FaqHomepageViewModel Faq { set; get; }
		public UpdateCommand(FaqHomepageViewModel faq)
		{
			this.Faq = faq;
		}
	}
}
