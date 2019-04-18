using Administrator.Commands.RoleCommands;
using MDM.UI.Roles.Interfaces;
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
    [Route("api/role")]
    [AuthorizeInUserService]
    public class RolesController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IRoleQueries roleQueries = null;
        private readonly IRoleRepository roleRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public RolesController(IMediator mediator, IRoleQueries roleQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.roleQueries = roleQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await roleQueries.GetsAsync();
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
            var rs = await roleQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertRoleCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateRoleCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteRoleCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
