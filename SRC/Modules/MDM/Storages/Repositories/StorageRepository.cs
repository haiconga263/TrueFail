using Common.Models;
using DAL;
using MDM.UI.Storages.Interfaces;
using MDM.UI.Storages.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Storages.Repositories
{
    public class StorageRepository : BaseRepository, IStorageRepository
    {
        public async Task<int> AddAsync(string key)
        {
            var storage = new Storage()
            {
                Key = key,
                NumericLength = 3,
                Prefix = "",
                Value = 1,
                HasDate = false,
            };
            var cmd = QueriesCreatingHelper.CreateQueryInsert(storage);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }
    }
}
