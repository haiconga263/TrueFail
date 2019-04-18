using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homepage.UI.ViewModels
{
	public class FaqHomepageViewModel : Faq
	{
		public List<FaqLanguage> FaqLanguages { get; set; } = new List<FaqLanguage>();
		public string QuestionDisplay { get; set; }
		public string AnswerDisplay { get; set; }
	}
}
