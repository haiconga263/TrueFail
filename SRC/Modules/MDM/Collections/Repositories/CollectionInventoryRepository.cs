using Common.Models;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Collections.Repositories
{
    public class CollectionInventoryRepository : BaseRepository, ICollectionInventoryRepository
    {
        public async Task<int> Add(CollectionInventory inventory)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(inventory);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(long inventoryId)
        {
            var cmd = $@"DELETE FROM `collection_inventory` WHERE `id` = {inventoryId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(CollectionInventory inventory)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(inventory);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
