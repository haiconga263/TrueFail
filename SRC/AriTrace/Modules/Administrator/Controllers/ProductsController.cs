using Administrator.Commands.ProductCommands;
using MDM.UI.Products.Interfaces;
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
    [Route("api/product")]
    [AuthorizeInUserService]
    public class ProductsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IProductQueries productQueries = null;
        private readonly IProductRepository productRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;

        public ProductsController(IMediator mediator, IProductQueries productQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.productQueries = productQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await productQueries.GetsAsync();
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
            var rs = await productQueries.GetByIdAsync(id);
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
            var rs = await productQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertProductCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateProductCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteProductCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
