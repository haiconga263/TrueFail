using Collections.Commands;
using Collections.PurchaseOrders.Commands;
using Collections.UI.Common;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Collections.PurchaseOrders.Controllers
{
    [Route(AppUrls.PrefixApiCollection + "/purchase-order")]
    [EnableCors("AllowSpecificOrigin")]
    public class PurchaseOrdersController : BaseController
    {
        private readonly IMediator mediator = null;
        public PurchaseOrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("check-changed")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> Changed([FromBody]CheckChangedCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("get")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> GetPOItems([FromBody]GetPOItemListCommand command)
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
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> InsertPO([FromBody]InsertPOCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("sync-farmer-order")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> InsertPOWithFarmOrder([FromBody]InsertPOWithFarmerOrderCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("insert-item")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> InsertPOItem([FromBody]InsertPOItemCommand command)
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
