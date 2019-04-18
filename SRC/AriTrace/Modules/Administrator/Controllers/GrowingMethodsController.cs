using Administrator.Commands.GrowingMethodCommands;
using MDM.UI.GrowingMethods.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Administrator.Controllers
{
    [Route("api/growing-method")]
    [AuthorizeInUserService]
    public class GrowingMethodsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IGrowingMethodQueries growingmethodQueries = null;
        private readonly IGrowingMethodRepository growingmethodRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public GrowingMethodsController(IMediator mediator, IGrowingMethodQueries growingmethodQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.growingmethodQueries = growingmethodQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await growingmethodQueries.GetsAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("getbyid")]
        public async Task<APIResult> GetById(int id)
        {
            var rs = await growingmethodQueries.GetByIdAsync(id);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("all")]
        public async Task<APIResult> GetAll()
        {
            var rs = await growingmethodQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertGrowingMethodCommand command)
        {
            var id = await mediator.Send(command);
            return new APIResult()
            {
                Data = new { id = (id > 0) ? id : (int?)null },
                Result = (id > 0) ? 0 : -1,
            };
        }

        [HttpPost]
        [Route("update")]
        public async Task<APIResult> Update([FromBody]UpdateGrowingMethodCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteGrowingMethodCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
