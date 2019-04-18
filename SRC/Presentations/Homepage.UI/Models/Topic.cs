using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("topic")]
	public class Topic : BaseModel
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("topic_name")]
		[Required]
		public string TopicName { set; get; }

		[Column("topic_url")]
		[Required]
		public string TopicUrl { set; get; }
		[Column("sort_by")]

		[Required]
		public int SortTopic { set; get; }

		[Column("is_used")]
		[Required]
		public bool IsUsed { set; get; }

		[Column("is_footer")]
		[Required]
		public bool IsFooter { set; get; }
	}
}
