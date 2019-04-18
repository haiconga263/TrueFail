using Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.UI.Messages.Interfaces
{
    public interface IMessagingService : IBaseService, IDisposable
    {
        Task SendScheduledMessageAsync(int messageId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
