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
    public class CollectionEmployeeQueries : BaseQueries, ICollectionEmployeeQueries
    {
        public async Task<IEnumerable<CollectionEmployee>> Gets(string condition = "")
        {
            return await DALHelper.ExecuteQuery<CollectionEmployee>($"SELECT * FROM `collection_employee` {(string.IsNullOrEmpty(condition) ? string.Empty : $"WHERE {condition}")}", dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<CollectionEmployee>> GetsByCollection(int collectionId)
        {
            return await DALHelper.ExecuteQuery<CollectionEmployee>($"SELECT * FROM `collection_employee` WHERE collection_id = {collectionId}", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
