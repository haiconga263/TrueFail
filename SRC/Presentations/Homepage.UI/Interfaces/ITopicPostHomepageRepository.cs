using Common.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface ITopicPostHomepageRepository : IBaseRepository
	{
		Task<int> Add(TopicPost topic);
		Task<int> Update(TopicPost topic);
		Task<int> Delete(int topicpostId);
	}
	
}
