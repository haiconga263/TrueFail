using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("topic_post")]
	public class TopicPost
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }


		[Column("topic_id")]
		public int TopicId { set; get; }


		[Column("post_id")]
		public int PostId { set; get; }


	}
}
