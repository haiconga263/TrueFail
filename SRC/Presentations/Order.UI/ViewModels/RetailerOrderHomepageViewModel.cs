using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Order.UI.Models;

namespace Order.UI.ViewModels
{
	public class RetailerOrderHomepageViewModel : RetailerOrderHomepage
	{
		//public decimal Amount => TotalPrice * TotalQuantity;
		//public decimal TotalPrice => RetailerOrderHomepageDetailViewModels.Sum(x => x.Price);
		//public decimal TotalQuantity => RetailerOrderHomepageDetailViewModels.Sum(x => x.Qty);
		public List<RetailerOrderHomepageDetailViewModel> RetailerOrderHomepageDetailViewModels { get; set; } = new List<RetailerOrderHomepageDetailViewModel>();
	}
}
