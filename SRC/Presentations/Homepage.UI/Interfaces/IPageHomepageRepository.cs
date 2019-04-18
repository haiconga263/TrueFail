using Common.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface IPageHomepageRepository : IBaseRepository
	{
		Task<int> AddAsync(Page page);
		Task<int> UpdateAsync(Page page);
		Task<int> DeleteAsync(Page page);
		Task<int> AddOrUpdateLanguage(PageLanguage language);
	}
}
