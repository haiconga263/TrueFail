using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.TopicCommand
{
	public class UpdateCommand : BaseCommand<int>
	{
		public TopicHomepageViewModel Topic { set; get; }
		public UpdateCommand(TopicHomepageViewModel topic)
		{
			this.Topic = topic;
		}
	}
}
