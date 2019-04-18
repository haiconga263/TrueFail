using Homepage.UI.Models;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.TopicCommand
{
	public class AddCommand : BaseCommand<int>
	{
		public TopicHomepageViewModel Topic { get; set; }
		public AddCommand(TopicHomepageViewModel topic)
		{
			this.Topic = topic;
		}
	}
}
