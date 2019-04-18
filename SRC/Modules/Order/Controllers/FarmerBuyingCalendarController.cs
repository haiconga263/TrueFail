using Common;
using Common.Exceptions;
using MDM.UI.Farmers.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.Commands.FarmerBuyingCalendar;
using Order.UI;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Web.Helpers;

namespace Order.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class FarmerBuyingCalendarController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries = null;
        public FarmerBuyingCalendarController(IMediator mediator, IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries)
        {
            this.mediator = mediator;
            this.farmerBuyingCalendarQueries = farmerBuyingCalendarQueries;
        }

        [HttpGet]
        [Route("is-changed")]
        [AuthorizeUser("Farmer")]
        public async Task<APIResult> IsChanged(DateTime? lastRequest)
        {
            var farmer = await WebHelper.HttpGet<FarmerViewModel>(GlobalConfiguration.APIGateWayURI, AppUrl.GetFarmerByUser, LoginSession.AccessToken);
            if (farmer == null || farmer.Id == 0)
                throw new BusinessException("Farmer.NotExisted");

            return new APIResult()
            {
                Result = 0,
                Data = new { isChanged = await farmerBuyingCalendarQueries.CheckChangedByFarmer(farmer?.Id ?? 0, lastRequest ?? DateTime.Now.AddDays(-1)) }
            };
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator,Farmer")]
        public async Task<APIResult> Gets(int farmerId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await farmerBuyingCalendarQueries.Gets(farmerId)
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Administrator,Farmer")]
        public async Task<APIResult> Get(long buyingCalendarId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await farmerBuyingCalendarQueries.Get(buyingCalendarId)
            };
        }

        #region un completed
        [HttpGet]
        [Route("gets/un-completed")]
        [AuthorizeUser("Administrator,Farmer")]
        public async Task<APIResult> GetsUnCompleted(int retailerId = 0)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator") && retailerId == 0)
            {
                throw new NotPermissionException();
            }
            string conditon = $"o.is_ordered = 0 and o.is_expired = 0";
            if (retailerId != 0)
            {
                conditon += " and o.retailer_id = " + retailerId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await farmerBuyingCalendarQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/un-completed/by-farmer")]
        [AuthorizeUser("Farmer")]
        public async Task<APIResult> GetsUnCompletedByFarmer()
        {
            var farmer = await WebHelper.HttpGet<FarmerViewModel>(GlobalConfiguration.APIGateWayURI, AppUrl.GetFarmerByUser, LoginSession.AccessToken);
            if (farmer == null || farmer.Id == 0)
                throw new BusinessException("Farmer.NotExisted");

            string conditon = $" o.is_ordered = 0 AND o.is_expired = 0 ";
            conditon += " AND o.farmer_id = " + farmer.Id;

            return new APIResult()
            {
                Result = 0,
                Data = await farmerBuyingCalendarQueries.Gets(conditon)
            };
        }
        #endregion

        [HttpGet]
        [Route("gets/completed")]
        [AuthorizeUser("Administrator,Farmer")]
        public async Task<APIResult> GetsCompleted(DateTime from, DateTime to, int retailerId = 0)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator") && retailerId == 0)
            {
                throw new NotPermissionException();
            }
            string conditon = $@"(o.is_ordered = 1 or o.is_expired = 1) and o.created_date >= '{from.Date.ToString("yyyyMMddHHmmss")}'
                                  and o.created_date < '{to.Date.AddSeconds(86399).ToString("yyyyMMddHHmmss")}'";
            if (retailerId != 0)
            {
                conditon += " and o.retailer_id = " + retailerId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await farmerBuyingCalendarQueries.Gets(conditon)
            };
        }

        [HttpPost]
        [Route("process/in-retailer-order")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> ProcessInRetailerOrder([FromBody]ProcessInRetailerOrderCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("process")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> Process([FromBody]ProcessCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("confirm-by-farmer")]
        [AuthorizeUser("Farmer")]
        public async Task<APIResult> ConfirmPlanningByFarmer([FromBody]ConfirmPlanningCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }
    }
}
