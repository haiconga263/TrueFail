using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.PostCommand
{
	public class UpdateCommand : BaseCommand<int>
	{
		public PostHomepageViewModel Post { set; get; }
		public UpdateCommand(PostHomepageViewModel post)
		{
			this.Post = post;
		}
	}
}
