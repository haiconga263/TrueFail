using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.PageCommand
{
	public class DeleteCommand : BaseCommand<int>
	{
		public int PageId { set; get; }
		public DeleteCommand(int pageId)
		{
			this.PageId = pageId;
		}

	}
}
