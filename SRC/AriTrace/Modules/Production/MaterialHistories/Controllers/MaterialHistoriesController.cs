using MediatR;
using Microsoft.AspNetCore.Mvc;
using Production.MaterialHistories.Commands;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Production.MaterialHistories.Controllers
{
    [Route("api/material-history")]
    [AuthorizeInUserService]
    public class MaterialHistoriesController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public MaterialHistoriesController(IMediator mediator, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpPost]
        [Route("getbyid")]
        public async Task<APIResult> GetById([FromBody]GetByIdMaterialHistoryCommand command)
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
        public async Task<APIResult> GetAll([FromBody]GetAllMaterialHistoryCommand command)
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
        public async Task<APIResult> Insert([FromBody]InsertMaterialHistoryCommand command)
        {
            var id = await mediator.Send(command);
            return new APIResult()
            {
                Data = new { id = (id > 0) ? id : (int?)null },
                Result = (id > 0) ? 0 : -1,
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteMaterialHistoryCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
