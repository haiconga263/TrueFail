using Common.Helpers;
using Common.Models;
using DAL;
using MDM.UI.Storages.Interfaces;
using MDM.UI.Storages.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Storages.Queries
{
    public class StorageQueries : BaseQueries, IStorageQueries
    {
        private readonly IStorageRepository _storageRepository;
        public StorageQueries(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task<string> GenarateCodeAsync(string key)
        {
            var storage = await GetValueAndIncOneAsync(key);
            return $"{storage.Prefix}{(storage.HasDate ? DateTime.Now.ToString("yyMMdd") : "")}{storage.Value.ToString().PadLeft(storage.NumericLength, '0')}";
        }

        public async Task<Storage> GetValueAndIncOneAsync(string key)
        {
            string cmd = $@"LOCK TABLES `storage` WRITE;
                            START TRANSACTION;

                            SELECT * FROM `storage` where `key`='{key}';
                            UPDATE `storage` SET `value` = `value` + 1 WHERE `key`='{key}';

                            COMMIT;
                            UNLOCK TABLES;";
            var storage = (await DALHelper.Query<Storage>(cmd, dbTransaction: DbTransaction, connection: DbConnection))
               .FirstOrDefault();
            if (storage == null)
            {
                await _storageRepository.AddAsync(key);
                return await GetValueAndIncOneAsync(key);
            }
            return storage;
        }
    }
}
