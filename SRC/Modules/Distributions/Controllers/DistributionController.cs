using Common;
using Common.Exceptions;
using Distributions.Commands.Manager;
using Distributions.UI;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Web.Helpers;

namespace Distributions.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class DistributionController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IDistributionQueries distributionQueries = null;
        public DistributionController(IMediator mediator, IDistributionQueries distributionQueries)
        {
            this.mediator = mediator;
            this.distributionQueries = distributionQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await distributionQueries.Gets()
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
                Data = await distributionQueries.Get(id)
            };
        }

        [HttpGet]
        [Route("gets/bysupervisor")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> GetBySupervisor()
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            return new APIResult()
            {
                Result = 0,
                Data = await distributionQueries.GetsBySupervisor(employee.Id)
            };
        }


        [HttpGet]
        [Route("gets/by-owner")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> GetsByOwner()
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            return new APIResult()
            {
                Result = 0,
                Data = await distributionQueries.GetsByEmployeeId(employee.Id)
            };
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeUser("Administrator")]
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
    }
}
