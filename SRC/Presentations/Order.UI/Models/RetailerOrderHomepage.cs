using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Common.Attributes;
using Common.Models;

namespace Order.UI.Models
{
	[Table("retailer_order_temp")]
	public class RetailerOrderHomepage
	{
		[Key]
		[Column("id")]
		public int Id { set; get; }

		[Column("name")]
		public string Name { set; get; }

		[Column("companyname")]
		public string CompanyName { set; get; }

		[Column("address")]
		public string Address { set; get; }
		[Column("email")]
		public string Email { set; get; }

		[Column("phonenumber")]
		public float PhoneNumber { set; get; }
		[Column("note")]
		public string Note { set; get; }
		[Column("buydate")]
		public DateTime BuyDate { set; get; }

		[Column("order_detail")]
		public string OrderDetail { get; set; }

		[Column("shipping_address")]
		public string ShippingAddress { get; set; }

		[Column("shipping_date")]
		public DateTime ShippingDate { set; get; }

	}

}
