using Notifications.Messages.Commands;
using Notifications.UI.Common;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Notifications.Messages.Controllers
{
    [Route(AppConfigs.PrefixApiFireBaseCloudMessaging)]
    [EnableCors("AllowSpecificOrigin")]
    public class CloudMessagingController : BaseController
    {
        private readonly IMediator mediator = null;
        public CloudMessagingController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("push-notification")]
        [AuthorizeUser("")]
        public async Task<APIResult> PushNotification([FromBody]PushNotificationCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }
    }
}
