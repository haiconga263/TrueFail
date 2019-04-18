using Homepage.UI.Models;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Homepage.Commands.PostCommand
{
	public class AddCommand : BaseCommand<int>
	{
		public PostHomepageViewModel Post { get; set; }
		public AddCommand(PostHomepageViewModel post)
		{
			this.Post = post;
		}
	}
}
