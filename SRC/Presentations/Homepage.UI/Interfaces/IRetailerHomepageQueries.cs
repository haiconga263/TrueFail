using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Homepage.UI.ViewModels;

namespace Homepage.UI.Interfaces
{
	public interface IRetailerHomepageQueries : IBaseQueries
	{
		Task<IEnumerable<RetailerHomepageViewModel>> GetRetailerAsync(string condition = "");
	}
}
