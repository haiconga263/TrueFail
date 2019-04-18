using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using DAL;
using Order.UI.Interfaces;
using Order.UI.Models;
using Order.UI.ViewModels;

namespace Order.Queries
{
	public class RetailerOrderHomepageQueries : BaseQueries, IRetailerOrderHomepageQueries
	{
		public async Task<IEnumerable<RetailerOrderHomepage>> Gets(string condition = "")
		{
			return await DALHelper.Query<RetailerOrderHomepage>($"SELECT * FROM `retailer_order_temp`", dbTransaction: DbTransaction, connection: DbConnection);
		}

		public async Task<IEnumerable<RetailerOrderHomepage>> Get(long id)
		{
			return await DALHelper.Query<RetailerOrderHomepage>($"SELECT * FROM `retailer_order_temp` re WHERE re.Id = `{id}`", dbTransaction: DbTransaction, connection: DbConnection);
		}
	}
}
