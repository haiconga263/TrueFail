using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Productions.Cultivations.Commands;
using Productions.UI.Cultivations;
using Productions.UI.Cultivations.Interfaces;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Productions.Cultivations.Controllers
{
    [Route(CultivationUrl.Prefix)]
    [EnableCors("AllowSpecificOrigin")]
    public class CultivationController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICultivationQueries cultivationQueries = null;
        public CultivationController(IMediator mediator, ICultivationQueries cultivationQueries)
        {
            this.mediator = mediator;
            this.cultivationQueries = cultivationQueries;
        }

        [HttpGet]
        [Route(CultivationUrl.get)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await cultivationQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(CultivationUrl.gets)]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await cultivationQueries.Gets()
            };
        }

        [HttpPost]
        [Route(CultivationUrl.insert)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Add([FromBody]AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(CultivationUrl.update)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Update([FromBody]UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route(CultivationUrl.delete)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Delete([FromBody]DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
