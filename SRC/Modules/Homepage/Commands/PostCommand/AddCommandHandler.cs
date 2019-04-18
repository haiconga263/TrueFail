using DAL;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Homepage.Commands.PostCommand
{
	public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
	{
		private readonly IPostHomepageRepository postRepository = null;
		private readonly ITopicPostHomepageQueries topicPostHomepageQueries = null;
		private readonly ITopicPostHomepageRepository topicPostHomepageRepository = null;
		public AddCommandHandler(IPostHomepageRepository postRepository, ITopicPostHomepageQueries topicPostHomepageQueries, ITopicPostHomepageRepository topicPostHomepageRepository)
		{
			this.postRepository = postRepository;
			this.topicPostHomepageQueries = topicPostHomepageQueries;
			this.topicPostHomepageRepository = topicPostHomepageRepository;
		}
		public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
		{
			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						request.Post = CreateBuild(request.Post, request.LoginSession);
						request.Post.IsUsed = true;
						var postId = await postRepository.AddAsync(request.Post);
						
						if(request.Post.Topics.Count() != 0)
						{
							var topicPost = new TopicPost();
							foreach (var topicItems in request.Post.Topics)
							{
								topicPost.PostId = postId;
								topicPost.TopicId = topicItems.Id;
								rs = (await topicPostHomepageRepository.Add(topicPost))>0 ? 0 : -1;
							}
						}
						else
						{
							var topicPost = new TopicPost()
							{
								PostId = postId,
								TopicId = 1
							};
							rs = (await topicPostHomepageRepository.Add(topicPost)) > 0 ? 0 : -1;
						}


						// languages
						foreach (var item in request.Post.PostLanguages)
						{
							item.PostId = postId;
							rs = (await postRepository.AddOrUpdateLanguage(item)) >0 ? 0 : -1;
						}
						

					}
					catch (Exception ex)
					{
						throw ex;
					}
					finally
					{
						if (rs == 0)
						{
							trans.Commit();
						}
						else
						{
							try
							{
								trans.Rollback();
							}
							catch { }
						}
					}
				}
			}

			return rs;
		}
	}
}
