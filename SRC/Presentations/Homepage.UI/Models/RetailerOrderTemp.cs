using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Common.Attributes;

namespace Homepage.UI.Models
{

	[Table("retailer_order_temp")]
	public class RetailerOrderTemp
	{
		[Key]
		[Identity]
		[Column("id")]
		public int Id { set; get; }

		[Column("name")]
		[Required]
		public string Name { set; get; }

		[Column("companyname")]
		[Required]
		public string CompanyName { set; get; }

		[Column("address")]
		[Required]
		public string Address { set; get; }
		[Column("email")]
		[Required]
		public string Email { set; get; }

		[Column("phonenumber")]
		[Required]
		public float PhoneNumber { set; get; }
		[Column("note")]
		[Required]
		public string Note { set; get; }
		[Column("buydate")]
		[Required]
		public DateTime BuyDate { set; get; }

		[Column("order_detail")]
		public string OrderDetail { get; set; }

		[Column("shipping_address")]
		[Required]
		public string ShippingAddress { get; set; }

		[Column("shipping_date")]
		[Required]
		public DateTime ShippingDate { set; get; }

	}
}
