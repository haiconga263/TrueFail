using Collections.Commands.Collections;
using Collections.UI;
using Common;
using Common.Exceptions;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Employees.Models;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.UI.ViewModels;
using System.Collections.Generic;
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
    public class CollectionController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICollectionQueries collectionQueries = null;
        public CollectionController(IMediator mediator, ICollectionQueries collectionQueries)
        {
            this.mediator = mediator;
            this.collectionQueries = collectionQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Gets()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await collectionQueries.Gets()
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
                Data = await collectionQueries.Get(id)
            };
        }

        [HttpGet]
        [Route("gets/by-owner")]
        [AuthorizeUser("Collector")]
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
                Data = await collectionQueries.GetsByEmployeeId(employee.Id)
            };
        }

        [HttpGet]
        [Route("gets/by-owner-manager")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetsByOwnerManager()
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            return new APIResult()
            {
                Result = 0,
                Data = await collectionQueries.GetsBySupervisor(employee.Id)
            };
        }

        [HttpGet]
        [Route("gets/orders")]
        [AuthorizeUser("Collector")]
        public async Task<APIResult> GetOrders(int collectionId)
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var collection = (await collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(c => c.Id == collectionId);
            if(collection == null)
            {
                throw new NotPermissionException();
            }

            var orders = await WebHelper.HttpGet<IEnumerable<FarmerOrderViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetFarmerOrderByCollection}?collectionId={collectionId}", LoginSession.AccessToken);

            return new APIResult()
            {
                Result = 0,
                Data = orders
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
