using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Productions.Plots.Commands;
using Productions.UI.Plots;
using Productions.UI.Plots.Interfaces;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Productions.Plots.Controllers
{
    [Route(PlotUrl.Prefix)]
    [EnableCors("AllowSpecificOrigin")]
    public class PlotController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IPlotQueries plotQueries = null;
        public PlotController(IMediator mediator, IPlotQueries plotQueries)
        {
            this.mediator = mediator;
            this.plotQueries = plotQueries;
        }

        [HttpGet]
        [Route(PlotUrl.get)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await plotQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(PlotUrl.gets)]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await plotQueries.Gets()
            };
        }

        [HttpPost]
        [Route(PlotUrl.insert)]
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
        [Route(PlotUrl.update)]
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
        [Route(PlotUrl.delete)]
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
