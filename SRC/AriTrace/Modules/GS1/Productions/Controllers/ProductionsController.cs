using GS1.Productions.Commands;
using GS1.UI.Productions.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace GS1.Productions.Controllers
{
    [Route("api/production")]
    [AuthorizeInUserService]
    public class ProductionsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IProductionQueries productionQueries = null;
        private readonly IProductionRepository productionRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public ProductionsController(IMediator mediator, IProductionQueries productionQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.productionQueries = productionQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets(GetCommonProductionCommand command)
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
        public async Task<APIResult> GetById([FromBody]GetByIdProductionCommand command)
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
        public async Task<APIResult> GetAll([FromBody]GetAllProductionCommand command)
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
        public async Task<APIResult> Insert([FromBody]InsertProductionCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateProductionCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteProductionCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
