using Collections.Configurations.Commands;
using Collections.UI.Common;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Collections.Configurations.Controllers
{
    [Route(AppUrls.PrefixApiCollection + "/config-app")]
    [EnableCors("AllowSpecificOrigin")]
    public class ConfigurationsController : BaseController
    {
        private readonly IMediator mediator = null;
        public ConfigurationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("get")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> GetConfig([FromBody]GetConfigsCommand command)
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
