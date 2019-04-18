using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("page_language")]
	public class PageLanguage
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("page_id")]
		[Required]
		public int PageId { set; get; }

		[Column("language_id")]
		[Required]
		public int LanguageId { set; get; }

		[Column("content")]
		[Required]
		public string Content { set; get; }

		[Column("title")]
		[Required]
		public string Title { set; get; }
	}
}
