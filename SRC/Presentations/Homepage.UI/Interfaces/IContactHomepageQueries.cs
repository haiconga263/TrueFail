using Common.Interfaces;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface IContactHomepageQueries : IBaseQueries
	{
		Task<IEnumerable<ContactHomepageViewModel>> GetAllContact();
		Task<ContactHomepageViewModel> GetContactById(int contactId, int languageId = 1);
	}
}
