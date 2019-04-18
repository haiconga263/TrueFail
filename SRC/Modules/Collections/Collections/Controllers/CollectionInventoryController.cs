using Collections.Commands.CollectionInventory;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Collections.Collections.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class CollectionInventoryController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICollectionInventoryQueries collectionInventoryQueries = null;
        public CollectionInventoryController(IMediator mediator, ICollectionInventoryQueries collectionInventoryQueries)
        {
            this.mediator = mediator;
            this.collectionInventoryQueries = collectionInventoryQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await collectionInventoryQueries.Gets()
            };
        }

        [HttpGet]
        [Route("gets/by-sku")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetsBySKU()
        {
            var inventories = await collectionInventoryQueries.Gets();
            var result = new List<CollectionInventoryBySKUViewModel>();
            foreach (var item in inventories)
            {
                var sku = result.FirstOrDefault(s => s.ProductId == item.ProductId && s.UoMId == item.UoMId);
                if(sku == null)
                {
                    result.Add(new CollectionInventoryBySKUViewModel()
                    {
                        ProductId = item.ProductId,
                        UoMId = item.UoMId,
                        Quantity = item.Quantity,
                        Details = new List<MDM.UI.Collections.Models.CollectionInventory>()
                        {
                            item
                        }
                    });
                }
                else
                {
                    sku.Quantity += item.Quantity;
                    sku.Details.Add(item);
                }
            }
            return new APIResult()
            {
                Result = 0,
                Data = result
            };
        }

        [HttpGet]
        [Route("get/by-tracecode")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> Get(string traceCode)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await collectionInventoryQueries.GetByTraceCode(traceCode)
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
    }
}
