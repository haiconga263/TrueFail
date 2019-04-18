using System.Linq;
using System.Threading.Tasks;
using AriSystem.UI.Storages.Interfaces;
using Common.Helpers;
using Common.Models;
using DAL;
using MDM.UI.Categories.Models;

namespace AriSystem.Storages.Queries
{
    public class StorageQueries : BaseQueries, IStorageQueries
    {
        public async Task<long> GetValueAndIncOneAsync(string key)
        {
            string cmd = $@"LOCK TABLES `storage` WRITE;
                            START TRANSACTION;

                            SELECT value FROM `storage` where `key`='{key}';
                            UPDATE `storage` SET `value` = `value` + 1 WHERE `key`='{key}';

                            COMMIT;
                            UNLOCK TABLES;";
            var value = (await DALHelper.Query<long?>(cmd, dbTransaction: DbTransaction, connection: DbConnection))
               .FirstOrDefault();
            return value?.ToLong() ?? 0;
        }

        public async Task<long> GetValueAsync(string key)
        {
            var value = (await DALHelper.Query<long?>($"SELECT value FROM `storage` WHERE `key` = {key}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault() ?? 0;
            return value;
        }
    }
}
