using Administrator.Commands.CaptionCommands;
using MDM.UI.Captions.Interfaces;
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
    [Route("api/caption")]
    [AuthorizeInUserService]
    public class CaptionsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICaptionQueries captionQueries = null;
        private readonly ICaptionRepository captionRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public CaptionsController(IMediator mediator, ICaptionQueries captionQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.captionQueries = captionQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("getbyid")]
        public async Task<APIResult> GetById(int id)
        {
            var rs = await captionQueries.GetByIdAsync(id);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await captionQueries.GetsAsync();
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
            var rs = await captionQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertCaptionCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateCaptionCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteCaptionCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
