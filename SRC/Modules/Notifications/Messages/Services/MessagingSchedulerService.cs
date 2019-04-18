using Notifications.Messages.Jobs;
using Notifications.UI.Messages.Interfaces;
using Notifications.UI.Messages.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Services;

namespace Notifications.Messages.Services
{
    public class MessagingSchedulerService : BaseService, IMessagingSchedulerService
    {
        private readonly IScheduler _scheduler;
        private readonly IMessagingRepository _messagingRepository;
        private readonly IMessagingQueries _messagingQueries;

        public MessagingSchedulerService(IScheduler scheduler, IMessagingRepository messagingRepository, IMessagingQueries messagingQueries)
        {
            this._scheduler = scheduler;
            this._messagingRepository = messagingRepository;
            this._messagingQueries = messagingQueries;
        }

        public async Task<Message> ScheduleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            var id = await _messagingRepository.AddAsync(message, cancellationToken);
            message = await _messagingQueries.GetByIdAsync(id);

            IJobDetail job = JobBuilder.Create<ProcessMessageJob>()
                .WithIdentity(Guid.NewGuid().ToString())
                .UsingJobData(ProcessMessageJob.JobDataKey, message.Id)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(Guid.NewGuid().ToString())
                .StartAt(message.ScheduledTime ?? DateTime.Now.AddSeconds(30))
                .Build();

            await _scheduler.ScheduleJob(job, trigger, cancellationToken);

            return message;
        }
    }
}
