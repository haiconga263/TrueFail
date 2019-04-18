using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using DAL;
using Homepage.UI.Interfaces;
using Homepage.UI.Models;

namespace Homepage.Repositories
{
	public class OrderHomepageRepository : BaseRepository, IOrderHomepageRepository
	{
		public async Task<int> AddAsync(RetailerOrderTemp retailerOrder)
		{
			var cmd = QueriesCreatingHelper.CreateQueryInsert(retailerOrder);
			cmd += ";SELECT LAST_INSERT_ID();";
			return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
		}
	}
}
