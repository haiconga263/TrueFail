using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Productions.Methods.Commands;
using Productions.UI.Methods;
using Productions.UI.Methods.Interfaces;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Productions.Methods.Controllers
{
    [Route(MethodUrl.Prefix)]
    [EnableCors("AllowSpecificOrigin")]
    public class MethodController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IMethodQueries methodQueries = null;
        public MethodController(IMediator mediator, IMethodQueries methodQueries)
        {
            this.mediator = mediator;
            this.methodQueries = methodQueries;
        }

        [HttpGet]
        [Route(MethodUrl.getAlls)]
        [AuthorizeUser()]
        public async Task<APIResult> GetAlls()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await methodQueries.GetAll()
            };
        }

        [HttpGet]
        [Route(MethodUrl.get)]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await methodQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route(MethodUrl.gets)]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await methodQueries.Gets()
            };
        }

        [HttpPost]
        [Route(MethodUrl.insert)]
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
        [Route(MethodUrl.update)]
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
        [Route(MethodUrl.delete)]
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
