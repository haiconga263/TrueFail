using Fulfillments.UI.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Fulfillments.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class FulfillmentFRController : BaseController
    {
        private readonly IFulfillmentFROrderQueries frQueries = null;
        public FulfillmentFRController(IFulfillmentFROrderQueries frQueries)
        {
            this.frQueries = frQueries;
        }
        [HttpGet]
        [Route("get/team")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> Get()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await frQueries.GetTeam()
            };
        }
        [HttpGet]
        [Route("get/retailerorderforpack")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> GetRetailerOrderForPack(string id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await frQueries.GetRetailerOrderForPackByFulId(id)
            };
        }

        [HttpGet]
        [Route("get/retailerorderbyid")]
        //[AuthorizeUser("Administrator")]
        public async Task<APIResult> GetRetailerOrderById(string id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await frQueries.GetRetailerOrderForPack(id)
            };
        }
    }
}
