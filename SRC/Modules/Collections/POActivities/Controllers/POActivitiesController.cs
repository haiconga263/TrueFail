using Collections.Commands;
using Collections.POActivities.Commands;
using Collections.UI.Common;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Collections.POActivities.Controllers
{
    [Route(AppUrls.PrefixApiCollection + "/activities")]
    [EnableCors("AllowSpecificOrigin")]
    public class POActivitiesController : BaseController
    {
        private readonly IMediator mediator = null;
        public POActivitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> GetList([FromBody]GetListPOActivityCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("insert")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> InsertActivity([FromBody]InsertActivityCommand command)
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
