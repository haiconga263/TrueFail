using Common.Models;
using DAL;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Queries
{
	public class TopicPostHomepageQueries : BaseQueries, ITopicPostHomepageQueries
	{
		
		public async Task<IEnumerable<TopicPost>> Gets(string condition = "")
		{
			return await DALHelper.ExecuteQuery<TopicPost>($"SELECT * FROM `topic_post` {(string.IsNullOrEmpty(condition) ? string.Empty : $"WHERE {condition}")}", dbTransaction: DbTransaction, connection: DbConnection);
		}

		public async Task<IEnumerable<TopicPost>> GetsByTopic(int topicId)
		{
			return await DALHelper.ExecuteQuery<TopicPost>($"SELECT * FROM `topic_post` WHERE topic_id = {topicId}", dbTransaction: DbTransaction, connection: DbConnection);
		}

		public async Task<IEnumerable<TopicPost>> GetsByPost(int postId)
		{
			return await DALHelper.ExecuteQuery<TopicPost>($"SELECT * FROM `topic_post` WHERE post_id = {postId}", dbTransaction: DbTransaction, connection: DbConnection);
		}
	}
}
