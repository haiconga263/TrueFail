using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Productions.Seeds.Commands;
using Productions.UI.Seeds;
using Productions.UI.Seeds.Interfaces;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Productions.Seeds.Controllers
{
    [Route(SeedUrl.Prefix)]
    [EnableCors("AllowSpecificOrigin")]
    public class SeedController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ISeedQueries seedQueries = null;
        public SeedController(IMediator mediator, ISeedQueries seedQueries)
        {
            this.mediator = mediator;
            this.seedQueries = seedQueries;
        }

        [HttpGet]
        [Route(SeedUrl.getAlls)]
        [AuthorizeUser()]
        public async Task<APIResult> GetAlls()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await seedQueries.GetAll()
            };
        }

        [HttpGet]
        [Route(SeedUrl.get)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await seedQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(SeedUrl.gets)]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await seedQueries.Gets()
            };
        }

        [HttpPost]
        [Route(SeedUrl.insert)]
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
        [Route(SeedUrl.update)]
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
        [Route(SeedUrl.delete)]
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
