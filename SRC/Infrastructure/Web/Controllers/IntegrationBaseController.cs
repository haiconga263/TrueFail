using Common.Interfaces;
using Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controls;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegrationBaseController<TCommand, TArrayCommand, TRequest> : BaseController where TCommand : IntegrationBaseCommand<TRequest>
                                                                                     where TArrayCommand : IntegrationArrayBaseCommand<TRequest>
                                                                                     where TRequest : BaseModel
    {
        private readonly IMediator _mediator = null;
        private readonly IIntegrationQueries<TRequest> queries = null;
        public IntegrationBaseController(IMediator mediator, IIntegrationQueries<TRequest> queries)
        {
            _mediator = mediator;
            this.queries = queries;
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Administrator")]
        public async Task<IActionResult> Get(int id)
        {
            // test function
            return Json(new APIResult()
            {
                Result = 0,
                Data = await queries.Get(id)
            });
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator")]
        public async Task<IActionResult> Gets()
        {
            // test function
            return Json(new APIResult()
            {
                Result = 0,
                Data = await queries.Gets()
            });
        }

        [HttpPost]
        [Route("insert")]
        [AuthorizeUser("Administrator")]
        public async Task<IActionResult> Post([FromBody]TCommand command)
        {
            command.Type = Common.IntergrationHandleType.Insert;
            return Json(new APIResult()
            {
                Result = await _mediator.Send(command)
            });
        }

        [HttpPost]
        [Route("update")]
        [AuthorizeUser("Administrator")]
        public async Task<IActionResult> PostUpdate([FromBody]TCommand command)
        {
            command.Type = Common.IntergrationHandleType.Update;
            return Json(new APIResult()
            {
                Result = await _mediator.Send(command)
            });
        }

        [HttpPost]
        [Route("delete")]
        [AuthorizeUser("Administrator")]
        public async Task<IActionResult> PostDelete([FromBody]TCommand command)
        {
            command.Type = Common.IntergrationHandleType.Delete;
            return Json(new APIResult()
            {
                Result = await _mediator.Send(command)
            });
        }

        [HttpPost]
        [Route("array")]
        [AuthorizeUser("Administrator")]
        public async Task<IActionResult> PostArray([FromBody]TArrayCommand command)
        {
            return Json(new APIResult()
            {
                Result = await _mediator.Send(command)
            });
        }
    }
}
