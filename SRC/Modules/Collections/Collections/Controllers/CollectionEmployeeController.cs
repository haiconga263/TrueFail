using Collections.Commands.CollectionEmployees;
using Collections.UI;
using Common;
using Common.Exceptions;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Employees.Models;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Web.Helpers;

namespace Collections.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class CollectionEmployeeController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICollectionEmployeeQueries collectionEmployeeQueries = null;
        private readonly ICollectionQueries collectionQueries = null;
        public CollectionEmployeeController(IMediator mediator, ICollectionEmployeeQueries collectionEmployeeQueries, ICollectionQueries collectionQueries)
        {
            this.mediator = mediator;
            this.collectionEmployeeQueries = collectionEmployeeQueries;
            this.collectionQueries = collectionQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator,DeliverySupervisor")]
        public async Task<APIResult> Gets(int collectionId)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator"))
            {
                var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
                if (employee == null)
                {
                    throw new NotPermissionException();
                }
                var distribution = (await this.collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(d => d.Id == collectionId);
                if (distribution == null)
                {
                    throw new NotPermissionException();
                }
            }

            return new APIResult()
            {
                Result = 0,
                Data = await collectionEmployeeQueries.GetsByCollection(collectionId)
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
