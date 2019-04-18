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
	public class TopicHomepageRepository : BaseRepository, ITopicHomepageRepository
	{
		public async Task<int> AddAsync(Topic topic)
		{
			var cmd = QueriesCreatingHelper.CreateQueryInsert(topic);
			cmd += ";SELECT LAST_INSERT_ID();";
			return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
		}

		public async Task<int> DeleteAsync(Topic topic)
		{
			var cmd = $@"UPDATE `topic`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {topic.ModifiedBy},
                         `modified_date` = '{topic.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {topic.Id}";
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}

		public async Task<int> UpdateAsync(Topic topic)
		{
			var cmd = QueriesCreatingHelper.CreateQueryUpdate(topic);
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}
		public async Task<int> AddOrUpdateLanguage(TopicLanguage language)
		{
			string cmd = QueriesCreatingHelper.CreateQuerySelect<TopicLanguage>($"topic_id = {language.TopicId} AND language_id = {language.LanguageId}");
			var lang = (await DALHelper.ExecuteQuery<TopicLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
			if (lang == null)
			{
				cmd = QueriesCreatingHelper.CreateQueryInsert(language);
				cmd += ";SELECT LAST_INSERT_ID();";
				return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			}
			else
			{
				language.Id = lang.Id;
				cmd = QueriesCreatingHelper.CreateQueryUpdate(language);
				var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
				return rs == 0 ? -1 : language.Id;
			}
		}
	}
}
