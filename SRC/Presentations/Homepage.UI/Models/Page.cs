using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("page")]
	public class Page : BaseModel
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("title")]
		[Required]
		public string Title { set; get; }


		[Column("content")]
		[Required]
		public string Content { set; get; }

		[Column("is_used")]
		[Required]
		public bool IsUsed { set; get; }

		[Column("is_pushing")]
		[Required]
		public bool IsPushing { set; get; }

		[Column("is_footer")]
		[Required]
		public bool IsFooter { set; get; }
	}
}
