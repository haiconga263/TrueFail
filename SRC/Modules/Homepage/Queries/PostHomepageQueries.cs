using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using Homepage.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Queries
{
	public class PostHomepageQueries : BaseQueries, IPostHomepageQueries
	{
		private string topic = $@"(SELECT *
						FROM topic tp
						LEFT JOIN `language` l ON l.code = 'en'
						LEFT JOIN `topic_language` tpl ON tpl.language_id = l.id AND tpl.topic_id = tp.id
						WHERE tp.`is_deleted` = 0 AND tp.`is_used` = '1' )";

		#region HomepageWeb
		public async Task<IEnumerable<PostHomepageViewModel>> GetAllPostByTopicId(int topicId, string lang = "vi")
		{
			List<PostHomepageViewModel> result = new List<PostHomepageViewModel>();
			string cmd = $@"SELECT p.*, tp.*, pl.* FROM `post` as p
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `post_language` pl ON p.id = pl.post_id AND pl.language_id = l.id
                            LEFT JOIN `topic` tp ON p.topic_id = tp.id 
                            WHERE p.is_deleted = 0 AND p.is_used = '1' AND tp.id = {topicId}";

			DbConnection = DbConnection ?? DALHelper.GetConnection();
			var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
			rd.Read<Post, Topic, PostLanguage, PostHomepageViewModel>(
				(p, tp, pl) =>
				{
					var post = result.FirstOrDefault(x => x.Id == p.Id);

					if (post == null)
					{
						post = CommonHelper.Mapper<Post, PostHomepageViewModel>(p);
						result.Add(post);
					}

					if (pl != null)
					{
						post.Content = string.IsNullOrWhiteSpace(pl.Content) ? post.Content : pl.Content;
						post.Description = string.IsNullOrWhiteSpace(pl.Description) ? post.Description : pl.Description;
						post.Title = string.IsNullOrWhiteSpace(pl.Title) ? post.Title : pl.Title;

					}
					else
					{
						post.Content = post.Content;
						post.Description = post.Description;
						post.Title = post.Title;
					}

					if (tp != null)
					{
						post.Topics.Add(tp);
					}

					return post;
				}
			);

			return result;

		}

		public async Task<IEnumerable<PostHomepageViewModel>> GetPostById(int postId, string lang = "vi")
		{
			List<PostHomepageViewModel> result = new List<PostHomepageViewModel>();
			string cmd = $@"SELECT p.*, pl.* FROM `post` as p
                            LEFT JOIN `language` l ON l.code = '{lang}'
                            LEFT JOIN `post_language` pl ON p.id = pl.post_id AND pl.language_id = l.id
                            WHERE p.id = {postId} AND p.is_deleted = 0 AND p.	is_used = '1'";

			DbConnection = DbConnection ?? DALHelper.GetConnection();
			var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
			rd.Read<Post, PostLanguage, PostHomepageViewModel>(
				(p, pl) =>
				{
					var post = result.FirstOrDefault(x => x.Id == p.Id);

					if (post == null)
					{
						post = CommonHelper.Mapper<Post, PostHomepageViewModel>(p);
						result.Add(post);
					}

					if (pl != null)
					{
						post.Content = string.IsNullOrWhiteSpace(pl.Content) ? post.Content : pl.Content;
						post.Description = string.IsNullOrWhiteSpace(pl.Description) ? post.Description : pl.Description;
						post.Title = string.IsNullOrWhiteSpace(pl.Title) ? post.Title : pl.Title;

					}
					else
					{
						post.Content = post.Content;
						post.Title = post.Title;
						post.Description = post.Description;
					}

					return post;
				}
			);

			return result;
		}

		public async Task<IEnumerable<TopicHomepageViewModel>> GetTopicFooter()
		{
			List<TopicHomepageViewModel> result = new List<TopicHomepageViewModel>();
			string cmd = $@"SELECT * FROM `topic` as tp 
                            LEFT JOIN `topic_type` tpt ON tp.topictype_id = tpt.id";
			//if (!string.IsNullOrWhiteSpace(condition))
			//{
			//	cmd += " AND " + condition;
			//}
			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Topic, TopicType, TopicHomepageViewModel>(
					(tp, tpt) =>
					{
						var topic = result.FirstOrDefault(p => p.Id == tp.Id);

						if (topic == null)
						{
							topic = CommonHelper.Mapper<Topic, TopicHomepageViewModel>(tp);
							result.Add(topic);
						}

						if (tpt != null)
						{
							topic.TopicTypes = tpt;
						}

						return topic;
					}
				);

				return result;
			}
			else
			{
				using (var conn = DALHelper.GetConnection())
				{
					var rd = await conn.QueryMultipleAsync(cmd);
					rd.Read<Topic, TopicType, TopicHomepageViewModel>(
						(tp, tpt) =>
						{
							var topic = result.FirstOrDefault(p => p.Id == tp.Id);

							if (topic == null)
							{
								topic = CommonHelper.Mapper<Topic, TopicHomepageViewModel>(tp);
								result.Add(topic);
							}

							if (tpt != null)
							{
								topic.TopicTypes = tpt;
							}

							return topic;
						}
					);
					return result;
				}

			}
		}

		public async Task<IEnumerable<FaqHomepageViewModel>> GetAllFaq(string lang = "vi")
		{
			string cmd = $@"SELECT * FROM `faq`";

			DbConnection = DbConnection ?? DALHelper.GetConnection();

			var result = await DbConnection.QueryAsync<FaqHomepageViewModel>(cmd, transaction: DbTransaction);


			return result;
		}
		#endregion

		#region HomepageAPI
		public async Task<IEnumerable<PostHomepageViewModel>> GetAllPost()
		{
			List<PostHomepageViewModel> result = new List<PostHomepageViewModel>();
			string cmd = $@"SELECT p.*, pl.* FROM `post` p
                            LEFT JOIN `post_language` pl ON p.id = pl.post_id
                            WHERE p.is_deleted = 0 ";
			//DbConnection = DbConnection ?? DALHelper.GetConnection();
			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Post, PostLanguage, PostHomepageViewModel>(
					(pRs, plRs) =>
					{
						var post = CommonHelper.Mapper<Post, PostHomepageViewModel>(pRs);
						var item = result.FirstOrDefault(x => x.Id == post.Id);
						if (item == null)
						{
							result.Add(post);
						}

						if (plRs != null)
						{
							var lang = post.PostLanguages.FirstOrDefault(l => l.Id == plRs.Id);
							if (lang == null)
							{
								post.PostLanguages.Add(plRs);
							}
						}

						return post;
					}
				);

				return result;
			}
			else
			{
				using (var conn = DALHelper.GetConnection())
				{
					var rd = await conn.QueryMultipleAsync(cmd);
					rd.Read<Post, PostLanguage, PostHomepageViewModel>(
					(pRs, plRs) =>
					{
						var post = CommonHelper.Mapper<Post, PostHomepageViewModel>(pRs);
						var item = result.FirstOrDefault(x => x.Id == post.Id);
						if (item == null)
						{
							result.Add(post);
						}

						if (plRs != null)
						{
							var lang = post.PostLanguages.FirstOrDefault(l => l.Id == plRs.Id);
							if (lang == null)
							{
								post.PostLanguages.Add(plRs);
							}
						}

						return post;
					}
				);

					return result;
				}
			}
		}

		public async Task<PostHomepageViewModel> GetPost(int postId, string lang = "vi")
		{
			PostHomepageViewModel result = null;
			string cmd = $@"SELECT p.*, top.*, pl.* FROM `post` p
							LEFT JOIN `topic_post` tp On tp.post_id = p.id
							LEFT JOIN `topic` top ON top.id = tp.topic_id
                            LEFT JOIN `post_language` pl ON p.id = pl.post_id
                            WHERE p.is_deleted = 0 and p.id = {postId}";

			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Post, Topic, PostLanguage,  PostHomepageViewModel>(
					(pRs, topicRs, plRs ) =>
					{
						if (result == null)
						{
							result = CommonHelper.Mapper<Post, PostHomepageViewModel>(pRs);
						}


						if (plRs != null)
						{

							result.PostLanguages.Add(plRs);

						}
						if (topicRs != null)
						{
							var topic = result.Topics.FirstOrDefault(tps => tps.Id == topicRs.Id);
							if (topic == null)
							{
								result.Topics.Add(topicRs);
							}
						}

						return result;
					}
				);

				return result;
			}
			else
			{
				using (var conn = DALHelper.GetConnection())
				{
					var rd = await conn.QueryMultipleAsync(cmd);
					rd.Read<Post, Topic, PostLanguage, PostHomepageViewModel>(
					(pRs, topicRs, plRs) =>
					{
						if (result == null)
						{
							result = CommonHelper.Mapper<Post, PostHomepageViewModel>(pRs);
						}


						if (plRs != null)
						{

							result.PostLanguages.Add(plRs);

						}
						if (topicRs != null)
						{
							var topic = result.Topics.FirstOrDefault(tps => tps.Id == topicRs.Id);
							if (topic == null)
							{
								result.Topics.Add(topicRs);
							}
						}

						return result;
					}
				);

					return result;
				}
			}
		}
		#endregion

	}
}
