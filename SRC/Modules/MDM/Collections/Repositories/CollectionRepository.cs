using Common.Models;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Collections.Repositories
{
    public class CollectionRepository : BaseRepository, ICollectionRepository
    {
        public async Task<int> Add(Collection collection)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(collection);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Collection collection)
        {
            var cmd = $@"UPDATE `collection`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {collection.ModifiedBy},
                         `modified_date` = '{collection.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {collection.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Collection collection)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(collection);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
