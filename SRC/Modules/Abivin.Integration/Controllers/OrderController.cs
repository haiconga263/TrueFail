using Abivin.Integration.Commands.OrderCommand;
using Abivin.Integration.UI;
using Abivin.Integration.UI.Interfaces;
using Common.Exceptions;
using Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Abivin.Integration.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class OrderController : BaseController
    {
        private readonly IAbivinOrderQueries abivinOrderQueries = null;
        private readonly IMediator mediator = null;
        public OrderController(IAbivinOrderQueries abivinOrderQueries, IMediator mediator)
        {
            this.abivinOrderQueries = abivinOrderQueries;
            this.mediator = mediator;
        }

        /// <summary>
        /// Get Order by Code
        /// </summary>
        /// <param name="key">The public key</param>
        /// <param name="code">OrderCode</param>
        /// <param name="sign">Sign: MD5(key + private key + code)</param>
        [HttpGet]
        [Route("get")]
        public async Task<APIResult> Get(string key, string code, string sign)
        {
            var signCheck = $"{key}{Const.AbivinPrivateKey}{code}".CreateMD5();
            if(!Const.AbivinPublicKey.Equals(key) || signCheck != sign)
            {
                throw new NotPermissionException();
            }
            return new APIResult()
            {
                Result = 0,
                Data = await abivinOrderQueries.Get(code)
            };
        }

        /// <summary>
        /// Get Order by date
        /// </summary>
        /// <param name="key">The public key</param>
        /// <param name="date">yyyyMMdd</param>
        /// <param name="sign">Sign: MD5(key + private key + date)</param>
        [HttpGet]
        [Route("gets")]
        public async Task<APIResult> Gets(string key, string date, string sign)
        {
            var signCheck = $"{key}{Const.AbivinPrivateKey}{date}".CreateMD5();
            if (!Const.AbivinPublicKey.Equals(key) || signCheck != sign)
            {
                throw new NotPermissionException();
            }

            return new APIResult()
            {
                Result = 0,
                Data = await abivinOrderQueries.Gets($"o.buying_date = '{date}' and o.status_id = {(int)RetailerOrderStatuses.InLogistic}")
            };
        }
        [HttpPost]
        [Route("update/status")]
        public async Task<APIResult> UpdateStatus([FromBody] ChangeOrderStatusCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }
    }
}
