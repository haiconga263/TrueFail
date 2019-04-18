using Collections.Commands.CollectionShippings;
using MDM.UI.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Collections.Collections.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class CollectionShippingController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICFShippingQueries cFShippingQueries = null;
        public CollectionShippingController(IMediator mediator, ICFShippingQueries cFShippingQueries)
        {
            this.mediator = mediator;
            this.cFShippingQueries = cFShippingQueries;
        }

        [HttpGet]
        [Route("gets/un-completed")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> Gets(int collectionId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await cFShippingQueries.Gets($"collection_id = {collectionId} and status_id not in ({(int)Distributions.UI.TripStatuses.Canceled}, {(int)Distributions.UI.TripStatuses.Finished})")
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> Get(long id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await cFShippingQueries.Get(id)
            };
        }

        [HttpGet]
        [Route("gets/item")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetItems(long shippingId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await cFShippingQueries.GetItems(shippingId)
            };
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeUser("Collector")]
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
        [AuthorizeUser("Collector")]
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
        [AuthorizeUser("Collector")]
        public async Task<APIResult> Delete([FromBody]DeleteCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/status")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> Delete([FromBody]UpdateStatusCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/items")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> UpdateItems([FromBody]UpdateItemCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
