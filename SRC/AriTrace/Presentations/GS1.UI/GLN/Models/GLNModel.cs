using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GS1.UI.GLN.Models
{
	[Table("gln")]
	public class GLNModel
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("code")]
		public string Code { get; set; }

		[Column("partner_id")]
		public int PartnerId { get; set; }

		//[Column("type")]
		//public GTINTypes Type { get; set; }

		[Column("used_date")]
		public DateTime UsedDate { get; set; }

		[Column("is_used")]
		public bool IsUsed { get; set; }

		[Column("is_public")]
		public bool IsPublic { get; set; }

		[Column("created_by")]
		public int CreatedBy { get; set; }

		[Column("created_date")]
		public DateTime CreatedDate { get; set; }

		[Column("modified_by")]
		public int ModifiedBy { get; set; }

		[Column("modified_date")]
		public DateTime ModifiedDate { get; set; }
		[Column("is_deleted")]
		public bool IsDeleted { get; set; }
		[Column("country_id")]
		public int CountryId { get; set; }
	}
}
