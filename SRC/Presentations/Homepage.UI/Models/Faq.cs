using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homepage.UI.Models
{
	[Table("faq")]
	public class Faq : BaseModel
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("question")]
		[Required]
		public string Question { set; get; }

		[Column("answer")]
		public string Answer { set; get; }

		[Column("is_used")]
		public bool IsUsed { set; get; }

	}
}
