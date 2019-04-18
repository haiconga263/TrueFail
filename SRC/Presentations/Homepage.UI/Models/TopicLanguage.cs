using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("topic_language")]
	public class TopicLanguage
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("topic_id")]
		[Required]
		public int TopicId { set; get; }

		[Column("language_id")]
		[Required]
		public int LanguageId { set; get; }

		[Column("topic_name")]
		[Required]
		public string TopicName { set; get; }

		[Column("topic_url")]
		[Required]
		public string TopicUrl { set; get; }

		
	}
}
