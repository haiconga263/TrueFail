using Common.Models;
using DAL;
using Fulfillments.UI.Interfaces;
using Fulfillments.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fulfillments.Repositories
{
    public class FulfillmentFrRepositories : BaseQueries, IFulfillmentFROrderRepository
    {
        public async Task<int> Add(FulfillmentFROrderViewModel fulFr)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(fulFr);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddItems(FulfillmentFROrderItemViewModel fulFrItems)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(fulFrItems);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }
    }
}
