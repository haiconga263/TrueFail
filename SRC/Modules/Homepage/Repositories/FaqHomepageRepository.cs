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
	public class FaqHomepageRepository : BaseRepository, IFaqHomepageRepository
	{
		public async Task<int> AddAsync(Faq faq)
		{
			var cmd = QueriesCreatingHelper.CreateQueryInsert(faq);
			cmd += ";SELECT LAST_INSERT_ID();";
			return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
		}

		public async Task<int> DeleteAsync(Faq faq)
		{
			var cmd = $@"UPDATE `faq`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {faq.ModifiedBy},
                         `modified_date` = '{faq.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {faq.Id}";
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}

		public async Task<int> UpdateAsync(Faq faq)
		{
			var cmd = QueriesCreatingHelper.CreateQueryUpdate(faq);
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}
		public async Task<int> AddOrUpdateLanguage(FaqLanguage language)
		{
			string cmd = QueriesCreatingHelper.CreateQuerySelect<FaqLanguage>($"faq_id = {language.FaqId} AND language_id = {language.LanguageId}");
			var lang = (await DALHelper.ExecuteQuery<FaqLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
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
