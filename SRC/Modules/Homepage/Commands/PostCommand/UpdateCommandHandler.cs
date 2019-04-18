using Common.Exceptions;
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
	public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
	{
		private readonly IPostHomepageRepository postRepository = null;
		private readonly IPostHomepageQueries postQueries = null;
		private readonly ITopicPostHomepageRepository topicPostHomepageRepository = null;
		private readonly ITopicPostHomepageQueries topicPostHomepageQueries = null;
		public UpdateCommandHandler(IPostHomepageRepository postRepository, IPostHomepageQueries postQueries,
			ITopicPostHomepageRepository topicPostHomepageRepository,
			ITopicPostHomepageQueries topicPostHomepageQueries)
		{
			this.postRepository = postRepository;
			this.postQueries = postQueries;
			this.topicPostHomepageRepository = topicPostHomepageRepository;
			this.topicPostHomepageQueries = topicPostHomepageQueries;
		}
		public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
		{
			if (request.Post == null || request.Post.Id == 0)
			{
				throw new BusinessException("Post.NotExisted");
			}

			var post = (await postQueries.GetPostById(request.Post.Id));
			if (post == null)
			{
				throw new BusinessException("Post.NotExisted");
			}

			var rs = -1;
			using (var conn = DALHelper.GetConnection())
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						//Update table Post
						request.Post = UpdateBuild(request.Post, request.LoginSession);
						rs = await postRepository.UpdateAsync(request.Post);

						//Get all topicpost of post update
						var topicPosts = await topicPostHomepageQueries.GetsByPost(request.Post.Id);
						if (topicPosts == null) return -1;

						//delete all topicpost of post  update by id
						foreach (var tpItem in topicPosts)
						{
							rs = await topicPostHomepageRepository.Delete(tpItem.Id);
						}

						//insert new topicpost of post update
						if (request.Post.Topics.Count() != 0)
						{
							var topicPost = new TopicPost();
							foreach (var topicItems in request.Post.Topics)
							{
								topicPost.PostId = request.Post.Id;
								topicPost.TopicId = topicItems.Id;
								rs = (await topicPostHomepageRepository.Add(topicPost)) > 0 ? 0 : -1;
							}
						}
						else
						{
							var topicPost = new TopicPost()
							{
								PostId = request.Post.Id,
								TopicId = 1
							};
							rs = (await topicPostHomepageRepository.Add(topicPost)) > 0 ? 0 : -1;
						}

						// languages
						foreach (var item in request.Post.PostLanguages)
						{
							item.PostId = request.Post.Id;
							rs = (await postRepository.AddOrUpdateLanguage(item)) >0 ?0 : -1;
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
