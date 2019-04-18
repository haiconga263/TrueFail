using Common.Interfaces;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface ITopicHomepageQueries : IBaseQueries
	{
		Task<IEnumerable<TopicHomepageViewModel>> GetAllTopic();
		Task<TopicHomepageViewModel> GetTopicById(int topicId, string lang = "vi");

	}
}
