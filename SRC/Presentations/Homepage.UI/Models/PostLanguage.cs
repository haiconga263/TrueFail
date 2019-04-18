using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("post_language")]
	public class PostLanguage
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("post_id")]
		[Required]
		public int PostId { set; get; }

		[Column("language_id")]
		[Required]
		public int LanguageId { set; get; }

		[Column("description")]
		[Required]
		public string Description { set; get; }

		[Column("content")]
		[Required]
		public string Content { set; get; }

		[Column("title")]
		[Required]
		public string Title { set; get; }
	}
}
