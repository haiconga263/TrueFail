using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Retailers.Models;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.Commands.RetailerBuyingCalendar;
using Order.UI;
using Order.UI.Interfaces;
using Order.UI.ViewModels;
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
    public class RetailerBuyingCalendarController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries = null;
        private readonly IRetailerOrderStatusQueries retailerOrderStatusQueries = null;
        public RetailerBuyingCalendarController(IMediator mediator, 
                                                IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries,
                                                IRetailerOrderStatusQueries retailerOrderStatusQueries)
        {
            this.mediator = mediator;
            this.retailerBuyingCalendarQueries = retailerBuyingCalendarQueries;
            this.retailerOrderStatusQueries = retailerOrderStatusQueries;
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> Gets(int retailerId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerBuyingCalendarQueries.Gets(retailerId)
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> Get(long buyingCalendarId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerBuyingCalendarQueries.Get(buyingCalendarId)
            };
        }

        [HttpGet]
        [Route("gets/un-completed")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> GetsUnCompleted(int retailerId = 0)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator") && retailerId == 0)
            {
                throw new NotPermissionException();
            }
            string conditon = $"o.is_ordered = 0 and o.is_expired = 0";
            if (retailerId != 0)
            {
                if (!LoginSession.Roles.Any(r => r == "Administrator"))
                {
                    var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={LoginSession.Id}", LoginSession.AccessToken);
                    if (retailer == null || retailer.Id != retailerId)
                    {
                        throw new NotPermissionException();
                    }
                }
                conditon += " and o.retailer_id = " + retailerId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await retailerBuyingCalendarQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/un-completed/byuser")]
        [AuthorizeUser("Retailer")]
        public async Task<APIResult> GetsUnCompletedByUser()
        {
            string conditon = $"o.is_ordered = 0 and o.is_expired = 0";
            var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={LoginSession.Id}", LoginSession.AccessToken);
            if (retailer == null)
            {
                throw new NotPermissionException();
            }
            conditon += " and o.retailer_id = " + retailer.Id;
            return new APIResult()
            {
                Result = 0,
                Data = await retailerBuyingCalendarQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/completed")]
        [AuthorizeUser("Administrator,Retailer")]
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
                if (!LoginSession.Roles.Any(r => r == "Administrator"))
                {
                    var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={LoginSession.Id}", LoginSession.AccessToken);
                    if (retailer == null || retailer.Id != retailerId)
                    {
                        throw new NotPermissionException();
                    }
                }
                conditon += " and o.retailer_id = " + retailerId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await retailerBuyingCalendarQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/byuser")]
        [AuthorizeUser("Retailer")]
        public async Task<APIResult> GetsByUser(DateTime from, DateTime to, PlanningStatusSearchFilter status, int userId)
        {
            string conditon = $@"o.created_date >= '{from.Date.ToString("yyyyMMddHHmmss")}'
                                 and o.created_date < '{to.Date.AddSeconds(86399).ToString("yyyyMMddHHmmss")}'
                                 and o.retailer_id = (SELECT r.id FROM `retailer` r WHERE r.user_account_id = {userId} and r.is_used = 1 and r.is_deleted = 0)";
            switch(status)
            {
                case PlanningStatusSearchFilter.All:
                    break;
                case PlanningStatusSearchFilter.Completed:
                    conditon += " and (o.is_ordered = 1 or o.is_expired = 1)";
                    break;
                case PlanningStatusSearchFilter.UnCompleted:
                    conditon += " and o.is_ordered = 0 and o.is_expired = 0";
                    break;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await retailerBuyingCalendarQueries.Gets(conditon)
            };
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> Add([FromBody]AddCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> Update([FromBody]UpdateCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update/adap")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> UpdateAdap([FromBody]UpdateAdapCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("delete")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> Delete([FromBody]DeleteCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }
    }
}
