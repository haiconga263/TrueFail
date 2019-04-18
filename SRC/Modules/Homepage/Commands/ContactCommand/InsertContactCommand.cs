using System;
using System.Collections.Generic;
using System.Text;
using Homepage.UI.ViewModels;
using Web.Controllers;

namespace Homepage.Commands.ContactCommand
{
	public class InsertContactCommand : BaseCommand<int>
	{
		public ContactHomepageViewModel Contact { set; get; }
		public InsertContactCommand(ContactHomepageViewModel contact)
		{
			Contact = contact;
		}
	}

}
