using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Productions.CultivationActivities.Commands;
using Productions.UI.CultivationActivities;
using Productions.UI.CultivationActivities.Interfaces;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Productions.CultivationActivities.Controllers
{
    [Route(CultivationActivityUrl.Prefix)]
    [EnableCors("AllowSpecificOrigin")]
    public class CultivationActivityController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICultivationActivityQueries cultivationActivityQueries = null;
        public CultivationActivityController(IMediator mediator, ICultivationActivityQueries cultivationActivityQueries)
        {
            this.mediator = mediator;
            this.cultivationActivityQueries = cultivationActivityQueries;
        }

        [HttpGet]
        [Route(CultivationActivityUrl.get)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await cultivationActivityQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(CultivationActivityUrl.gets)]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await cultivationActivityQueries.Gets()
            };
        }

        [HttpPost]
        [Route(CultivationActivityUrl.insert)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Add([FromBody]AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
