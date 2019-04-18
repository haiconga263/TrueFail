using Common.Interfaces;
using Common.Models;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.UI.Interfaces
{
	public interface IPostHomepageQueries : IBaseQueries
	{
		Task<IEnumerable<TopicHomepageViewModel>> GetTopicFooter();
		Task<IEnumerable<PostHomepageViewModel>> GetPostById(int postId, string lang = "vi");
		Task<IEnumerable<PostHomepageViewModel>> GetAllPostByTopicId(int topicId, string lang = "vi");
		Task<IEnumerable<FaqHomepageViewModel>> GetAllFaq(string lang = "vi");

		Task<IEnumerable<PostHomepageViewModel>> GetAllPost();
		Task<PostHomepageViewModel> GetPost(int postId, string lang = "vi");


	}
}
