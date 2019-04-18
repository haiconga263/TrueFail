using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("topic_type")]
	public class Tag : BaseModel
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("slug")]
		[Required]
		public string Slug { set; get; }

		[Column("name")]
		[Required]
		public string Name { set; get; }
	}
}
