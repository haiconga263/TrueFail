using Common.Models;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Collections.Queries
{
    public class CollectionInventoryQueries : BaseQueries, ICollectionInventoryQueries
    {
        public async Task<CollectionInventory> Get(string condition = "")
        {
            return (await DALHelper.ExecuteQuery<CollectionInventory>($"SELECT * FROM `collection_inventory` {(string.IsNullOrEmpty(condition) ? string.Empty : $"WHERE {condition}")}", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<CollectionInventory> GetByTraceCode(string code)
        {
            return (await DALHelper.ExecuteQuery<CollectionInventory>($"SELECT * FROM `collection_inventory` WHERE trace_code = '{code}'", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<CollectionInventory>> Gets(string condition = "")
        {
            return await DALHelper.ExecuteQuery<CollectionInventory>($"SELECT * FROM `collection_inventory` {(string.IsNullOrEmpty(condition) ? string.Empty : $"WHERE {condition}")}", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
