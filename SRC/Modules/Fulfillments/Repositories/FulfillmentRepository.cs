using Common.Models;
using DAL;
using Fulfillments.UI.Interfaces;
using Fulfillments.UI.Models;
using Fulfillments.UI.ViewModels;
using MDM.UI.Fulfillments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fulfillments.Repositories
{
    public class FulfillmentRepository : BaseRepository, IFulfillmentCollectionRepository
    {
        public async Task<int> Add(FulfillmentCollection fulCol)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(fulCol);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddItems(FulfillmentCollectionItem fulCoItem)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(fulCoItem);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }


		public async Task<int> PrintReceipt()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(FulfillmentCollection fulCol)
        {
            string cmd = QueriesCreatingHelper.CreateQueryUpdate(fulCol);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs != 0;
        }

        //public async Task<bool> UpdateItems(FulfillmentCollectionViewModel fulColItem)
        //{
        //    string cmd = QueriesCreatingHelper.CreateQueryUpdate(fulColItem);
        //    var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        //    return rs != 0;
        //}
	}
}
