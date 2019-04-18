using MDM.UI.UoMs.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class UoMController : BaseController
    {
        private readonly IUoMQueries uoMQueries = null;
        public UoMController(IUoMQueries uoMQueries)
        {
            this.uoMQueries = uoMQueries;
        }

        [HttpGet]
        [Route("gets")]
        public async Task<APIResult> Gets()
        {
            var rs = await uoMQueries.Gets();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }
    }
}
