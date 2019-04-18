using Common.Models;
using DAL;
using Order.UI.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Queries
{
    public class RetailerOrderStatusQueries : BaseQueries, IRetailerOrderStatusQueries
    {
        public async Task<IEnumerable<RetailerOrderStatus>> Gets()
        {
            return await DALHelper.Query<RetailerOrderStatus>($"SELECT * FROM `retailer_order_status`", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
