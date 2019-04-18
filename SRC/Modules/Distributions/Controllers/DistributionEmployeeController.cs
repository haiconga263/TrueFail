using Common;
using Common.Exceptions;
using Distributions.Commands.DistributionEmployees;
using Distributions.UI;
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
    public class DistributionEmployeeController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IDistributionEmployeeQueries distributionEmployeeQueries = null;
        private readonly IDistributionQueries distributionQueries = null;
        public DistributionEmployeeController(IMediator mediator, IDistributionEmployeeQueries distributionEmployeeQueries, IDistributionQueries distributionQueries)
        {
            this.mediator = mediator;
            this.distributionEmployeeQueries = distributionEmployeeQueries;
            this.distributionQueries = distributionQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator,DeliverySupervisor")]
        public async Task<APIResult> Gets(int distributionId)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator"))
            {
                var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
                if (employee == null)
                {
                    throw new NotPermissionException();
                }
                var distribution = (await this.distributionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(d => d.Id == distributionId);
                if (distribution == null)
                {
                    throw new NotPermissionException();
                }
            }

            return new APIResult()
            {
                Result = 0,
                Data = await distributionEmployeeQueries.GetsByDistribution(distributionId)
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
