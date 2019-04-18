using Notifications.UI.Messages.Enumerations;
using Notifications.UI.Messages.Interfaces;
using Notifications.UI.Messages.Models;
using Common.Models;
using DAL;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.Messages.Repositories
{
    public class MessagingRepository : BaseRepository, IMessagingRepository
    {
        private readonly IStorageQueries _storageQueries;

        public MessagingRepository(IStorageQueries storageQueries)
        {
            _storageQueries = storageQueries;
        }

        public async Task<int> AddAsync(Message msg, CancellationToken cancellationToken = default(CancellationToken))
        {
            msg.Code = await _storageQueries.GenarateCodeAsync(StorageKeys.MessageCode);
            var cmd = QueriesCreatingHelper.CreateQueryInsert(msg);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<bool> DeleteAsync(int id, int modifiedBy, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cmd = $@"UPDATE `{MessageConfigs.TABLE_NAME}`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {modifiedBy},
                         `modified_date` = '{DateTime.Now.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs != 0;
        }

        public async Task<bool> UpdateAsync(Message msg, CancellationToken cancellationToken = default(CancellationToken))
        {
            string cmd = QueriesCreatingHelper.CreateQueryUpdate(msg);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs != 0;
        }
    }
}
