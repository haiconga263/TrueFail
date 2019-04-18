using Common.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface ITopicPostHomepageQueries : IBaseQueries
	{
		Task<IEnumerable<TopicPost>> Gets(string condition = "");
		Task<IEnumerable<TopicPost>> GetsByTopic(int topicId);
		Task<IEnumerable<TopicPost>> GetsByPost(int postId);
	}
}
