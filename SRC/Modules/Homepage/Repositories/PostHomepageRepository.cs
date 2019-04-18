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
	public class PostHomepageRepository : BaseRepository, IPostHomepageRepository
	{
		public async Task<int> AddAsync(Post post)
		{
			var cmd = QueriesCreatingHelper.CreateQueryInsert(post);
			cmd += ";SELECT LAST_INSERT_ID();";
			return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
		}

		public async Task<int> DeleteAsync(Post post)
		{
			var cmd = $@"UPDATE `post`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {post.ModifiedBy},
                         `modified_date` = '{post.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {post.Id}";
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}

		public async Task<int> UpdateAsync(Post post)
		{
			var cmd = QueriesCreatingHelper.CreateQueryUpdate(post);
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}
		public async Task<int> AddOrUpdateLanguage(PostLanguage language)
		{
			string cmd = QueriesCreatingHelper.CreateQuerySelect<PostLanguage>($"post_id = {language.PostId} AND language_id = {language.LanguageId}");
			var lang = (await DALHelper.ExecuteQuery<PostLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
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
