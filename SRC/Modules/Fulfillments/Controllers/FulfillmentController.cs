using Fulfillments.Commands;
using Fulfillments.Commands.FCCommand;
using Fulfillments.UI.Interfaces;
using MDM.UI.Fulfillments.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;


namespace Fulfillments.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class FulfillmentController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IFulfillmentQueries fulfillmentQueries = null;
        private readonly IFulfillmentCollectionQueries queries = null;
        
        public FulfillmentController(IMediator mediator, IFulfillmentQueries fulfillmentQueries,
            IFulfillmentCollectionQueries queries)
        {
            this.mediator = mediator;
            this.fulfillmentQueries = fulfillmentQueries;
            this.queries = queries;


		}

        [HttpGet]
        [Route("gets")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await fulfillmentQueries.Gets()
            };
        }

        [HttpGet]
        [Route("gets/actived")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetActiveds()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await fulfillmentQueries.Gets("f.is_used = 1")
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
                Data = await fulfillmentQueries.Get(id)
            };
        }

        [HttpPost]
        [Route("add")]
        //[AuthorizeUser("Administrator")]
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

        [HttpPost]
        [Route("get/odercollection")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> GetFromCollection()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await queries.GetOrderFromCollection()
            };
        }

        //[HttpPost]
        //[Route("get/detailco")]
        ////[AuthorizeUser("Administrator")]
        //public async Task<APIResult> GetDetail()
        //{

        //    return new APIResult()
        //    {
        //        Result = 0,
        //        Data = await queries.GetDetail()
        //    };
        //}
        [HttpPost]
        [Route("get/collection")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> GetCollection()
        {

            return new APIResult()
            {
                Result = 0,
                Data = await queries.GetCollection()
            };
        }
        [HttpPost]
        [Route("get/FulfillmentById")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> GetFulfillmentById(string id)
        {

            return new APIResult()
            {
                Result = 0,
                Data = await queries.GetOrderFromCollectionById(id)
            };
        }

        [HttpPost]
        [Route("get/fulfillmentCollectionByFcId")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> fulfillmentCollectionByFcId(string id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await queries.GetOrderFromCollectionByFcId(id)
            };
        }

        [HttpPost]
        [Route("add/fulfillmentCollection")]
		[AuthorizeUser("Administrator")]
		public async Task<APIResult> AddFulfillmentCollection([FromBody]AddFcCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("get/fulfillmentCollectionStatus")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> GetFulfillmentCollectStatus()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await queries.GetFulfillmentCollectionStatus()
            };
        }
        [HttpGet]
        [Route("get/getfcproduct")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> GetAllFCProduct()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await queries.GetAllFCProduct()
            };
        }
    }
}
