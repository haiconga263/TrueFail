using Common.Interfaces;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface IPageHomepageQueries : IBaseQueries
	{
		Task<IEnumerable<PageHomepageViewModel>> GetAllPage(string condition = "");
		Task<PageHomepageViewModel> GetPageByIdAsync(int pageId);

	}
}
