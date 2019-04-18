using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.ContactCommand
{
	public class DeleteContactCommand : BaseCommand<int>
	{
		public int ContactId { set; get; }
		public DeleteContactCommand(int contactId)
		{
			ContactId = contactId;
		}

	}
}