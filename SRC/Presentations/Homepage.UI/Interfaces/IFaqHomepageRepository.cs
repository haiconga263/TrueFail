using Common.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface IFaqHomepageRepository : IBaseRepository
	{
		Task<int> AddAsync(Faq faq);
		Task<int> UpdateAsync(Faq faq);
		Task<int> DeleteAsync(Faq faq);
		Task<int> AddOrUpdateLanguage(FaqLanguage language);
	}
}
