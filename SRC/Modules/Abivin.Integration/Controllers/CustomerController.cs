using Abivin.Integration.UI;
using Abivin.Integration.UI.Interfaces;
using Common.Exceptions;
using Common.Helpers;
using Common.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Abivin.Integration.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class CustomerController : BaseController
    {
        private readonly IAbivinCustomerQueries abivinCustomerQueries = null;
        public CustomerController(IAbivinCustomerQueries abivinCustomerQueries)
        {
            this.abivinCustomerQueries = abivinCustomerQueries;
        }

        /// <summary>
        /// Get Customer by Code
        /// </summary>
        /// <param name="key">The public key</param>
        /// <param name="code">CustomerCode</param>
        /// <param name="sign">Sign: MD5(key + private key + code)</param>
        [HttpGet]
        [Route("get")]
        public async Task<APIResult> Gets(string key, string code, string sign)
        {
            var signCheck = $"{key}{Const.AbivinPrivateKey}{code}".CreateMD5();
            if (!Const.AbivinPublicKey.Equals(key) || signCheck != sign)
            {
                throw new NotPermissionException();
            }
            return new APIResult()
            {
                Result = 0,
                Data = await abivinCustomerQueries.Get(code)
            };
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <param name="key">The public key</param>
        /// <param name="sign">Sign: MD5(key + private key)</param>
        [HttpGet]
        [Route("gets")]
        public async Task<APIResult> Gets(string key, string sign)
        {
            var signCheck = $"{key}{Const.AbivinPrivateKey}".CreateMD5();
            if (!Const.AbivinPublicKey.Equals(key) || signCheck != sign)
            {
                throw new NotPermissionException();
            }

            return new APIResult()
            {
                Result = 0,
                Data = await abivinCustomerQueries.Gets()
            };
        }
    }
}
