using Common.Models;
using DAL;
using GS1.UI.GLN.Interfaces;
using GS1.UI.GLN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1.GLN.Repositories
{
	public class GLNRepository : BaseRepository, IGLNRepository
	{
		public async Task<int> AddAsync(GLNModel gln)
		{
			var cmd = QueriesCreatingHelper.CreateQueryInsert(gln);
			cmd += ";SELECT LAST_INSERT_ID();";
			return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
		}

		public async Task<int> DeleteAsync(int id)
		{
			var cmd = $"delete from `gln` WHERE `id` = {id}";
			return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
		}

		public async Task<int> UpdateAsync(GLNModel gln)
		{
			var cmd = QueriesCreatingHelper.CreateQueryUpdate(gln);
			return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
		}
	}
}
