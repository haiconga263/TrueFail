using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.TopicCommand
{
	public class DeleteCommand : BaseCommand<int>
	{
		public int TopicId { set; get; }
		public DeleteCommand(int topicId)
		{
			this.TopicId = topicId;
		}

	}
}
