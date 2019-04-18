using GS1.Processes.Commands;
using GS1.UI.Processes.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace GS1.Processes.Controllers
{
    [Route("api/process")]
    [AuthorizeInUserService]
    public class ProcessesController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IProcessQueries processQueries = null;
        private readonly IProcessRepository processRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public ProcessesController(IMediator mediator, IProcessQueries processQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.processQueries = processQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets(GetCommonProcessCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("getbyid")]
        public async Task<APIResult> GetById([FromBody]GetByIdProcessCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("all")]
        public async Task<APIResult> GetAll([FromBody]GetAllProcessCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("new")]
        public async Task<APIResult> Insert([FromBody]NewProcessCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0,
            };
        }

        [HttpPost]
        [Route("update")]
        public async Task<APIResult> Update([FromBody]UpdateProcessCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteProcessCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
