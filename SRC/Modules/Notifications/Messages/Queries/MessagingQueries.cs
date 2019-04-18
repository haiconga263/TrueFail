using Notifications.UI.Messages.Enumerations;
using Notifications.UI.Messages.Interfaces;
using Notifications.UI.Messages.Models;
using Common.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.Messages.Queries
{
    public class MessagingQueries : BaseQueries, IMessagingQueries
    {
        public async Task<Message> GetByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
        {
            string cmd = $@"SELECT * FROM aritnt.`{MessageConfigs.TABLE_NAME}` m
		                        WHERE m.`id` = '{id}' AND m.`is_deleted` = 0";
            return (await DALHelper.Query<Message>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<Message>> GetsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            string cmd = $@"SELECT * FROM aritnt.`{MessageConfigs.TABLE_NAME}` m
		                        WHERE  m.`is_deleted` = 0";
            return await DALHelper.Query<Message>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
