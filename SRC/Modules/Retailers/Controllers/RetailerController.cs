using MDM.UI.Retailers.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Retailers.Commands.Commands.Retailers;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Retailers.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class RetailerController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IRetailerQueries retailerQueries = null;
        public RetailerController(IMediator mediator, IRetailerQueries retailerQueries)
        {
            this.mediator = mediator;
            this.retailerQueries = retailerQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerQueries.Gets()
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Retailer,Administrator")]
        public async Task<APIResult> Get(int retailerId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerQueries.Get(retailerId)
            };
        }

        [HttpGet]
        [Route("get/byuser")]
        [AuthorizeUser("Retailer")]
        public async Task<APIResult> GetByUser(int userId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerQueries.GetByUserId(userId)
            };
        }

        [HttpGet]
        [Route("gets/location")]
        [AuthorizeUser("Retailer,Administrator")]
        public async Task<APIResult> GetLocations(int retailerId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerQueries.GetRetailerLocations(retailerId)
            };
        }

        [HttpGet]
        [Route("gets/location/byuser")]
        [AuthorizeUser("Retailer")]
        public async Task<APIResult> GetLocationsByUser(int userId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerQueries.GetRetailerLocationsByUser(userId)
            };
        }

        [HttpGet]
        [Route("get/location")]
        [AuthorizeUser("Retailer,Administrator,DeliveryMan,DeliverySupervisor")]
        public async Task<APIResult> GetLocation(int locationId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerQueries.GetRetailerLocation(locationId)
            };
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeUser("Retailer,Administrator")]
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
        [AuthorizeUser("Retailer,Administrator")]
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
        [Route("add/location")]
        [AuthorizeUser("Retailer,Administrator")]
        public async Task<APIResult> AddLocation([FromBody]AddLocationCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update/location")]
        [AuthorizeUser("Retailer,Administrator")]
        public async Task<APIResult> UpdateLocation([FromBody]UpdateLocationCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete/location")]
        [AuthorizeUser("Retailer,Administrator")]
        public async Task<APIResult> DeleteLocation([FromBody]DeleteLocationCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
