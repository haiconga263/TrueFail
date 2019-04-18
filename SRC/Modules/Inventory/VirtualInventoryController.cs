using Inventory.UI.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Inventory
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class VirtualInventoryController : BaseController
    {
        private readonly IFarmerInventoryQueries farmerInventoryQueries = null;
        public VirtualInventoryController(IFarmerInventoryQueries farmerInventoryQueries)
        {
            this.farmerInventoryQueries = farmerInventoryQueries;
        }

        [HttpGet]
        [Route("get/by-product")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> GetByProduct(int productId, DateTime effect)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await farmerInventoryQueries.GetByProductId(productId, effect)
            };
        }
    }
}
