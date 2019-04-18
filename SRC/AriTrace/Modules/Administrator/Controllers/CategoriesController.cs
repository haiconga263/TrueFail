using Administrator.Commands.CategoryCommands;
using MDM.UI.Categories.Interfaces;
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
    [Route("api/category")]
    [AuthorizeInUserService]
    public class CategoriesController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICategoryQueries categoryQueries = null;
        private readonly ICategoryRepository categoryRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public CategoriesController(IMediator mediator, ICategoryQueries categoryQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.categoryQueries = categoryQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await categoryQueries.GetsAsync();
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
            var rs = await categoryQueries.GetByIdAsync(id);
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
            var rs = await categoryQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertCategoryCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateCategoryCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteCategoryCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
