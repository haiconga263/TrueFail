using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("faq_language")]
	public class FaqLanguage
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("faq_id")]
		[Required]
		public int FaqId { set; get; }

		[Column("language_id")]
		[Required]
		public int LanguageId { set; get; }

		[Column("question")]
		[Required]
		public string Question { set; get; }

		[Column("answer")]
		[Required]
		public string Answer { set; get; }
	}
}
