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
	public class PageHomepageRepository : BaseRepository, IPageHomepageRepository
	{
		public async Task<int> AddAsync(Page page)
		{
			var cmd = QueriesCreatingHelper.CreateQueryInsert(page);
			cmd += ";SELECT LAST_INSERT_ID();";
			return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
		}

		public async Task<int> DeleteAsync(Page page)
		{
			var cmd = $@"UPDATE `page`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {page.ModifiedBy},
                         `modified_date` = '{page.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {page.Id}";
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}

		public async Task<int> UpdateAsync(Page page)
		{
			var cmd = QueriesCreatingHelper.CreateQueryUpdate(page);
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}
		public async Task<int> AddOrUpdateLanguage(PageLanguage language)
		{
			string cmd = QueriesCreatingHelper.CreateQuerySelect<PageLanguage>($"page_id = {language.PageId} AND language_id = {language.LanguageId}");
			var lang = (await DALHelper.ExecuteQuery<PageLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
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
