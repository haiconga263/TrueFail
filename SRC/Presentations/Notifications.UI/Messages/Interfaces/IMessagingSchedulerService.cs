using Notifications.UI.Messages.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.UI.Messages.Interfaces
{
    public interface IMessagingSchedulerService
    {
        Task<Message> ScheduleMessageAsync(Message message, CancellationToken cancellationToken);
    }
}
