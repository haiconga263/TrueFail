using Common.Models;
using DAL;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Repositories
{
	public class TopicPostHomepageRepository : BaseRepository, ITopicPostHomepageRepository
	{
		public async Task<int> Add(TopicPost topic)
		{
			var cmd = QueriesCreatingHelper.CreateQueryInsert(topic);
			cmd += ";SELECT LAST_INSERT_ID();";
			return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
		}

		public async Task<int> Delete(int topicPostId)
		{
			var cmd = $@"DELETE FROM `topic_post` WHERE id = {topicPostId}";
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}

		public async Task<int> Update(TopicPost topic)
		{
			var cmd = QueriesCreatingHelper.CreateQueryUpdate(topic);
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}
	}
}
