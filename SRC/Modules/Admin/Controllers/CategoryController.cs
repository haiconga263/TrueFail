using Admin.Commands.CategoryCommand;
using MDM.UI.Categories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class CategoryController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICategoryQueries categoryQueries = null;
        public CategoryController(IMediator mediator, ICategoryQueries categoryQueries)
        {
            this.mediator = mediator;
            this.categoryQueries = categoryQueries;
        }

        [HttpGet]
        [Route("gets/all")]
        [AuthorizeUser()]
        public async Task<APIResult> GetAlls()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await categoryQueries.GetAll(LoginSession.LanguageCode)
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await categoryQueries.GetById(id)
            };
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser()]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await categoryQueries.Gets(LoginSession.LanguageCode)
            };
        }

        [HttpGet]
        [Route("gets/hierarchy")]
        [AuthorizeUser()]
        public async Task<APIResult> GetsHierarchy()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await categoryQueries.GetsWithChild()
            };
        }

        [HttpPost]
        [Route("insert")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Add([FromBody]AddCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Update([FromBody]UpdateCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Delete([FromBody]DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
