using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.ContactCommand
{
	public class UpdateContactCommand : BaseCommand<int>
	{
		public ContactHomepageViewModel Contact { set; get; }
		public UpdateContactCommand(ContactHomepageViewModel contact)
		{
			Contact = contact;
		}
	}
}
