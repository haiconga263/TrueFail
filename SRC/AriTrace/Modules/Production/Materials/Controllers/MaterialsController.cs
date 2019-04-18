using MDM.UI.Accounts.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Production.Materials.Commands;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Production.Materials.Controllers
{
    [Route("api/material")]
    [AuthorizeInUserService]
    public class MaterialsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public MaterialsController(IMediator mediator, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets(GetCommonMaterialCommand command)
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
        public async Task<APIResult> GetById([FromBody]GetByIdMaterialCommand command)
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
        public async Task<APIResult> GetAll([FromBody]GetAllMaterialCommand command)
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
        public async Task<APIResult> Insert([FromBody]InsertMaterialCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateMaterialCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteMaterialCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("generate-code")]
        public async Task<APIResult> GenerateCode([FromBody]GenerateCodeMaterialCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }
    }
}
