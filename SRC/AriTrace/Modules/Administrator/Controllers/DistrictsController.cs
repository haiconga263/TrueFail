using Administrator.Commands.DistrictCommands;
using MDM.UI.Districts.Interfaces;
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
    [Route("api/district")]
    [AuthorizeInUserService]
    public class DistrictsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IDistrictQueries districtQueries = null;
        private readonly IDistrictRepository districtRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public DistrictsController(IMediator mediator, IDistrictQueries districtQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.districtQueries = districtQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await districtQueries.GetsAsync();
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
            var rs = await districtQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertDistrictCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateDistrictCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteDistrictCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
