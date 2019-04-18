using Admin.Commands.EmployeeCommand;
using MDM.UI.Employees.Interfaces;
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

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class EmployeeController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IEmployeeQueries employeeQueries = null;
        public EmployeeController(IMediator mediator, IEmployeeQueries employeeQueries)
        {
            this.mediator = mediator;
            this.employeeQueries = employeeQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator,DeliverySupervisor,Collector")]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await employeeQueries.GetEmployees()
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Administrator,DeliverySupervisor,Collector")]
        public async Task<APIResult> Get(int employeeId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await employeeQueries.GetEmployee(employeeId)
            };
        }

        [HttpGet]
        [Route("get/byuser")]
        [AuthorizeUser()]
        public async Task<APIResult> GetByUser()
        {
            return new APIResult()
            {
                Result = 0,
                Data = (await employeeQueries.GetEmployees($"user_account_id = {LoginSession.Id}")).FirstOrDefault()
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
