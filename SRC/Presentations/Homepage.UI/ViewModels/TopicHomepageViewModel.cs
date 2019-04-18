using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homepage.UI.ViewModels
{
	public class TopicHomepageViewModel : Topic
	{
		public TopicType TopicTypes { get; set; } = new TopicType();
		public List<TopicLanguage> TopicLanguages { get; set; } = new List<TopicLanguage>();
	}
}

