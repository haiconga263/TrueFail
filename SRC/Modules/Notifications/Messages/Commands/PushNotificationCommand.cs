using Notifications.UI.Messages.Interfaces;
using Notifications.UI.Messages.Models;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Notifications.Messages.Commands
{
    public class PushNotificationCommand : BaseCommand<long>
    {
        public Message Message { get; set; }
        public PushNotificationCommand(Message message)
        {
            Message = message;
        }
    }

    public class PushNotificationCommandHandler : BaseCommandHandler<PushNotificationCommand, long>
    {
        private readonly IMessagingSchedulerService _schedulerService = null;

        public PushNotificationCommandHandler(IMessagingSchedulerService schedulerService)
        {
            _schedulerService = schedulerService;
        }

        public override async Task<long> HandleCommand(PushNotificationCommand request, CancellationToken cancellationToken)
        {
            long rs = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Message.Id = 0;
                        request.Message.IsDeleted = false;
                        request.Message.ModifiedBy = request.LoginSession.Id;
                        request.Message.ModifiedDate = DateTime.Now;
                        request.Message.CreatedBy = request.LoginSession.Id;
                        request.Message.CreatedDate = DateTime.Now;
                        var msg = await _schedulerService.ScheduleMessageAsync(request.Message, cancellationToken);
                        rs = msg?.Id ?? 0;
                    }
                    finally
                    {
                        if (rs > 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                        }
                    }
                }
            }

            return rs;
        }
    }
}

