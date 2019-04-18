using Common.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface  ITopicHomepageRepository : IBaseRepository
	{
		Task<int> AddAsync(Topic topic);
		Task<int> UpdateAsync(Topic topic);
		Task<int> DeleteAsync(Topic topic);
		Task<int> AddOrUpdateLanguage(TopicLanguage language);
	}
}
