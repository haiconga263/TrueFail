using Administrator.Commands.WardCommands;
using MDM.UI.Wards.Interfaces;
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
    [Route("api/ward")]
    [AuthorizeInUserService]
    public class WardsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IWardQueries wardQueries = null;
        private readonly IWardRepository wardRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public WardsController(IMediator mediator, IWardQueries wardQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.wardQueries = wardQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await wardQueries.GetsAsync();
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
            var rs = await wardQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertWardCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateWardCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteWardCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
