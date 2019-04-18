using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.PostCommand
{
	public class DeleteCommand : BaseCommand<int>
	{
		public int PostId { set; get; }
		public DeleteCommand(int postId)
		{
			this.PostId = postId;
		}

	}
}
