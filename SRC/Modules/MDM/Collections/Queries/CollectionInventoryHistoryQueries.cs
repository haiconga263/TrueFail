using Common.Models;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Collections.Queries
{
    public class CollectionInventoryHistoryQueries : BaseQueries, ICollectionInventoryHistoryQueries
    {
        public Task<IEnumerable<CollectionInventoryHistory>> Gets(string condition = "")
        {
            return DALHelper.ExecuteQuery<CollectionInventoryHistory>($"SELECT * FROM `collection_inventory_history` {(string.IsNullOrEmpty(condition) ? string.Empty : $"WHERE {condition}")}", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
