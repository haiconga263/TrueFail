using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("post")]
	public class Post : BaseModel
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("title")]
		[Required]
		public string Title { set; get; }

		[Column("description")]
		[Required]
		public string Description { set; get; }

		[Column("link")]
		[Required]
		public string Link { set; get; }

		[Column("content")]
		[Required]
		public string Content { set; get; }

		[Column("image_url")]
		[Required]
		public string ImageUrl { set; get; }

		[Column("vote")]
		[Required]
		public int Vote { set; get; }

		[Column("tag")]
		[Required]
		public string Tag { set; get; }

		[Column("is_used")]
		[Required]
		public bool IsUsed { set; get; }
	}
}
