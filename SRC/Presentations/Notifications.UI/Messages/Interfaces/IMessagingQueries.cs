using Notifications.UI.Messages.Models;
using Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.UI.Messages.Interfaces
{
    public interface IMessagingQueries : IBaseQueries
    {
        Task<Message> GetByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<Message>> GetsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
