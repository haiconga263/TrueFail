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
    public class CollectionInventoryHistoryRepository : BaseRepository, ICollectionInventoryHistoryRepository
    {
        public async Task<int> Add(CollectionInventoryHistory history)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(history);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }
    }
}
