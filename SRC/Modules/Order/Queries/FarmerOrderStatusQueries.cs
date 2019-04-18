using Common.Models;
using DAL;
using Order.UI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Queries
{
    public class FarmerOrderStatusQueries : BaseQueries, IFarmerOrderStatusQueries
    {
        public async Task<IEnumerable<Order.UI.Models.FarmerOrderStatus>> Gets()
        {
            return await DALHelper.Query<Order.UI.Models.FarmerOrderStatus>($"SELECT * FROM `farmer_order_status`", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
