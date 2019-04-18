using Common.Models;
using DAL;
using Order.UI.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Queries
{
    public class FarmerRetailerOrderItemQueries : BaseQueries, IFarmerRetailerOrderItemQueries
    {
        public async Task<FarmerRetailerOrderItems> Get(long id)
        {
            return (await DALHelper.ExecuteQuery<FarmerRetailerOrderItems>($"SELECT * FROM farmer_retailer_order_items WHERE id = {id}", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<FarmerRetailerOrderItems>> GetByBC(long bc)
        {
            return await DALHelper.ExecuteQuery<FarmerRetailerOrderItems>($"SELECT * FROM farmer_retailer_order_items WHERE is_planning = 1 and farmer_order_id = {bc}", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
