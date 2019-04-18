using Notifications.UI.Messages.Interfaces;
using Quartz;
using System.Threading.Tasks;

namespace Notifications.Messages.Jobs
{
    public class ProcessMessageJob : IJob
    {
        public static readonly string JobDataKey = "MESSAGE_ID";

        private readonly IMessagingService messagingService;

        public ProcessMessageJob(IMessagingService messagingService)
        {
            this.messagingService = messagingService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var cancellationToken = context.CancellationToken;
            var messageId = GetMessageId(context);

            await messagingService.SendScheduledMessageAsync(messageId, cancellationToken);
        }

        private int GetMessageId(IJobExecutionContext context)
        {
            JobDataMap jobDataMap = context.JobDetail.JobDataMap;

            return jobDataMap.GetIntValue(JobDataKey);
        }
    }
}
