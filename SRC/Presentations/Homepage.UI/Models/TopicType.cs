using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("topic_type")]
	public class TopicType
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("name")]
		[Required]
		public string TopicTypeName { set; get; }
		[Column("order_by")]
		[Required]
		public int SortType { set; get; }
	}
}
