using Common;
using Common.Exceptions;
using Distributions.Commands.Routers;
using Distributions.UI;
using Distributions.UI.Interfaces;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
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
using Web.Helpers;

namespace Distributions.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class RouterController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IRouterQueries routerQueries = null;
        private readonly IDistributionQueries distributionQueries = null;
        public RouterController(IMediator mediator, IRouterQueries routerQueries, IDistributionQueries distributionQueries)
        {
            this.mediator = mediator;
            this.routerQueries = routerQueries;
            this.distributionQueries = distributionQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> Gets(int distributionId)
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if(employee == null)
            {
                throw new NotPermissionException();
            }
            var distribution = (await this.distributionQueries.GetsBySupervisor(employee.Id)).FirstOrDefault(d => d.Id == distributionId);
            if(distribution == null)
            {
                throw new NotPermissionException();
            }
            return new APIResult()
            {
                Result = 0,
                Data = await routerQueries.Gets(distribution.Id)
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> Get(int id)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await routerQueries.Get(id)
            };
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeUser("DeliverySupervisor")]
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
        [AuthorizeUser("DeliverySupervisor")]
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
        [AuthorizeUser("DeliverySupervisor")]
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
