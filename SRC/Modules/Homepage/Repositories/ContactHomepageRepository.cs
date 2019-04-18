using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using DAL;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;

namespace Homepage.Repositories
{
	public class ContactHomepageRepository : BaseRepository, IContactHomepageRepository
	{
		public async Task<int> AddAsync(Contact contact)
		{
			var cmd = QueriesCreatingHelper.CreateQueryInsert(contact);
			cmd += ";SELECT LAST_INSERT_ID();";
			return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
		}
		public async Task<int> UpdateAsync(Contact contact)
		{
			var cmd = QueriesCreatingHelper.CreateQueryUpdate(contact);
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}
		public async Task<int> DeleteAsync(Contact contact)
		{
			var cmd = $@"UPDATE `hp_contact`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {contact.ModifiedBy},
                         `modified_date` = '{contact.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {contact.Id}";
			var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
			return rs == 0 ? -1 : 0;
		}
	}
}
