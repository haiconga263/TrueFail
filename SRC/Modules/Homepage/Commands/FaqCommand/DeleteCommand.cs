using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.FaqCommand
{
	public class DeleteCommand : BaseCommand<int>
	{
		public int FaqId { set; get; }
		public DeleteCommand(int faqId)
		{
			this.FaqId = faqId;
		}

	}
}
