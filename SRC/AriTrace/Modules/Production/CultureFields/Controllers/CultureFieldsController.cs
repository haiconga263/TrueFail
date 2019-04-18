using Production.UI.CultureFields.Interfaces;
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
using Production.CultureFields.Commands;

namespace Production.CultureFields.Controllers
{
    [Route("api/culture-field")]
    [AuthorizeInUserService]
    public class CultureFieldsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICultureFieldQueries cultureFieldQueries = null;
        private readonly ICultureFieldRepository cultureFieldRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public CultureFieldsController(IMediator mediator, ICultureFieldQueries cultureFieldQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.cultureFieldQueries = cultureFieldQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await cultureFieldQueries.GetsAsync();
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
            var rs = await cultureFieldQueries.GetByIdAsync(id);
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
            var rs = await cultureFieldQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertCultureFieldCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateCultureFieldCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteCultureFieldCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
