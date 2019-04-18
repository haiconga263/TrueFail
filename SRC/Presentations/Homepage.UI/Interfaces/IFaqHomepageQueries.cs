using Common.Interfaces;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface IFaqHomepageQueries : IBaseQueries
	{
		Task<IEnumerable<FaqHomepageViewModel>> GetAllFaq(string condition = "");
		Task<FaqHomepageViewModel> GetFaqByIdAsync(int faqId);

	}
}
