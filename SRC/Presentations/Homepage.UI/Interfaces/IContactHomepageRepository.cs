using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Homepage.UI.Models;

namespace Homepage.UI.Interfaces
{
	public interface IContactHomepageRepository : IBaseRepository
	{
		Task<int> AddAsync(Contact contact);
		Task<int> UpdateAsync(Contact contact);
		Task<int> DeleteAsync(Contact contact);
	}
}
