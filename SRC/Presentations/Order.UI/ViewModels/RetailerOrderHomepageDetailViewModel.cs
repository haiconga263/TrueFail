using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
	public class RetailerOrderHomepageDetailViewModel 
	{
		public int Id { set; get; }
		public string Name { set; get; }
		public string Image { get; set; }
		public int Qty { get; set; }
		public decimal Price { get; set; }

	}
}
