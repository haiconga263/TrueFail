using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homepage.UI.ViewModels
{
	public class PostHomepageViewModel : Post
	{
		public List<Topic> Topics { get; set; } = new List<Topic>();
		public List<PostLanguage> PostLanguages { get; set; } = new List<PostLanguage>();
	}
}
