using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Order.UI.Models;
using Order.UI.ViewModels;

namespace Order.UI.Interfaces
{
	public interface  IRetailerOrderHomepageQueries : IBaseQueries
	{
		Task<IEnumerable<RetailerOrderHomepage>> Gets(string condition = "");
		Task<IEnumerable<RetailerOrderHomepage>> Get(long id);
	}
}
