using Notifications.Messages.Converters;
using Notifications.UI.Common;
using Notifications.UI.Messages.Enumerations;
using Notifications.UI.Messages.Interfaces;
using Notifications.UI.Messages.Models;
using Common;
using Common.Helpers;
using FcmSharp;
using FcmSharp.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.Messages.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly IFcmClient _fcmClient;
        private readonly IMessagingQueries _messagingQueries;
        private readonly IMessagingRepository _messagingRepository;

        public MessagingService(IMessagingQueries messagingQueries, IMessagingRepository messagingRepository)
        {
            string serviceAccountKeyPath = GlobalConfiguration.GetByKey(AppConfigs.ServiceAccountKeyPathConfigKey);
            var settings = FileBasedFcmClientSettings.CreateFromFile(serviceAccountKeyPath);

            _fcmClient = new FcmClient(settings);
            _messagingQueries = messagingQueries;
            _messagingRepository = messagingRepository;
        }

        public async Task SendScheduledMessageAsync(int messageId, CancellationToken cancellationToken = default(CancellationToken))
        {
            LogHelper.GetLogger().Debug($"Sending scheduled Message ID {messageId}");

            var message = await _messagingQueries.GetByIdAsync(messageId, cancellationToken);

            if (message.Status == MessageStatus.Scheduled)
                await SendMessageAsync(message, cancellationToken);
        }

        private async Task SendMessageAsync(Message message, CancellationToken cancellationToken = default(CancellationToken))
        {
            var target = MessageConverter.Convert(message);

            try
            {
                await _fcmClient.SendAsync(target, cancellationToken);

                LogHelper.GetLogger().Debug($"Finished sending Message ID {message.Id}");
                message.Status = MessageStatus.Finished;
                await _messagingRepository.UpdateAsync(message, cancellationToken);
            }
            catch (Exception exception)
            {
                LogHelper.GetLogger().Error($"Error sending Message ID {message.Id}", exception);

                message.Status = MessageStatus.Failed;
                await _messagingRepository.UpdateAsync(message, cancellationToken);
            }
        }

        public void Dispose()
        {
            _fcmClient?.Dispose();
        }

        public void Run()
        {
            //throw new NotImplementedException();
        }
    }
}
