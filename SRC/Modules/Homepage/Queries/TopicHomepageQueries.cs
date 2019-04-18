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
	public class TopicHomepageQueries : BaseQueries, ITopicHomepageQueries
	{
		public async Task<IEnumerable<TopicHomepageViewModel>> GetAllTopic()
		{
			List<TopicHomepageViewModel> result = new List<TopicHomepageViewModel>();
			string cmd = $@"SELECT t.*, tl.* FROM `topic` t
                            LEFT JOIN `topic_language` tl ON t.id = tl.topic_id
                            WHERE t.is_deleted = 0 ";
			//DbConnection = DbConnection ?? DALHelper.GetConnection();
			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Topic, TopicLanguage, TopicHomepageViewModel>(
					(fRs, flRs) =>
					{
						var topic = CommonHelper.Mapper<Topic, TopicHomepageViewModel>(fRs);
						var item = result.FirstOrDefault(x => x.Id == topic.Id);
						if (item == null)
						{
							result.Add(topic);
						}

						if (flRs != null)
						{
							var lang = topic.TopicLanguages.FirstOrDefault(l => l.Id == flRs.Id);
							if (lang == null)
							{
								topic.TopicLanguages.Add(flRs);
							}
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
					rd.Read<Topic, TopicLanguage, TopicHomepageViewModel>(
					(fRs, flRs) =>
					{
						var topic = CommonHelper.Mapper<Topic, TopicHomepageViewModel>(fRs);
						var item = result.FirstOrDefault(x => x.Id == topic.Id);
						if (item == null)
						{
							result.Add(topic);
						}

						if (flRs != null)
						{
							var lang = topic.TopicLanguages.FirstOrDefault(l => l.Id == flRs.Id);
							if (lang == null)
							{
								topic.TopicLanguages.Add(flRs);
							}
						}

						return topic;
					}
				);

					return result;
				}
			}
		}

		public async Task<TopicHomepageViewModel> GetTopicById(int topicId, string lang = "vi")
		{
			TopicHomepageViewModel result = null;
			string cmd = $@"SELECT * FROM `topic` t
                            LEFT JOIN `topic_language` tl ON t.id = tl.topic_id
                            WHERE t.is_deleted = 0 and t.id = {topicId}";

			if (DbConnection != null)
			{
				var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
				rd.Read<Topic, TopicLanguage, TopicHomepageViewModel>(
					(fRs, flRs) =>
					{
						if (result == null)
						{
							result = CommonHelper.Mapper<Topic, TopicHomepageViewModel>(fRs);
						}


						if (flRs != null)
						{

							result.TopicLanguages.Add(flRs);

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
					rd.Read<Topic, TopicLanguage, TopicHomepageViewModel>(
					(fRs, flRs) =>
					{
						if (result == null)
						{
							result = CommonHelper.Mapper<Topic, TopicHomepageViewModel>(fRs);
						}

						if (flRs != null)
						{

							result.TopicLanguages.Add(flRs);

						}

						return result;
					}
				);

					return result;
				}
			}
		}
	}
}
