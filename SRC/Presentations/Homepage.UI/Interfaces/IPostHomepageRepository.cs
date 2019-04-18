using Common.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface IPostHomepageRepository : IBaseRepository
	{
		Task<int> AddAsync(Post post);
		Task<int> UpdateAsync(Post post);
		Task<int> DeleteAsync(Post post);
		Task<int> AddOrUpdateLanguage(PostLanguage language);
	}
}
