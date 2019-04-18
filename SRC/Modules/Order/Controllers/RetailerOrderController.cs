using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Distributions.ViewModels;
using MDM.UI.Employees.Models;
using MDM.UI.Retailers.Models;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.Commands.RetailerOrder;
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
    public class RetailerOrderController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IRetailerOrderQueries retailerOrderQueries = null;
        private readonly IRetailerOrderStatusQueries retailerOrderStatusQueries = null;
        public RetailerOrderController(IMediator mediator, 
                                       IRetailerOrderQueries retailerOrderQueries,
                                       IRetailerOrderStatusQueries retailerOrderStatusQueries)
        {
            this.mediator = mediator;
            this.retailerOrderQueries = retailerOrderQueries;
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
                Data = await retailerOrderQueries.Gets(retailerId)
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Administrator,Retailer,DeliveryMan")]
        public async Task<APIResult> Get(long orderId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await retailerOrderQueries.Get(orderId)
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
            string conditon = $"o.status_id <> {(int)RetailerOrderStatuses.Canceled} and o.status_id <> {(int)RetailerOrderStatuses.Completed}";
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
                Data = await retailerOrderQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/un-completed/byuser")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> GetsUnCompletedByUser()
        {
            string conditon = $"o.status_id <> {(int)RetailerOrderStatuses.Canceled} and o.status_id <> {(int)RetailerOrderStatuses.Completed}";
            if (!LoginSession.Roles.Any(r => r == "Administrator"))
            {
                var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={LoginSession.Id}", LoginSession.AccessToken);
                if (retailer == null)
                {
                    throw new NotPermissionException();
                }
                conditon += " and o.retailer_id = " + retailer.Id;
            }

            return new APIResult()
            {
                Result = 0,
                Data = await retailerOrderQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/completed")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> GetsCompleted(DateTime from, DateTime to, int retailerId = 0)
        {
            string conditon = $@"(o.status_id in ({((int)RetailerOrderStatuses.Canceled)}, {((int)RetailerOrderStatuses.Completed)})) and o.buying_date >= '{from.Date.ToString("yyyyMMddHHmmss")}'
                                  and o.buying_date < '{to.Date.AddSeconds(86399).ToString("yyyyMMddHHmmss")}'";
            if (retailerId != 0)
            {
                conditon += " and o.retailer_id = " + retailerId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await retailerOrderQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/completed/byuser")]
        [AuthorizeUser("Retailer")]
        public async Task<APIResult> GetsCompletedByUser(DateTime from, DateTime to)
        {
            string conditon = $@"(o.status_id in ({((int)RetailerOrderStatuses.Canceled)}, {((int)RetailerOrderStatuses.Completed)})) and o.buying_date >= '{from.Date.ToString("yyyyMMddHHmmss")}'
                                  and o.buying_date < '{to.Date.AddSeconds(86399).ToString("yyyyMMddHHmmss")}'";
            var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={LoginSession.Id}", LoginSession.AccessToken);
            if (retailer == null)
            {
                throw new NotPermissionException();
            }
            conditon += " and o.retailer_id = " + retailer.Id;

            return new APIResult()
            {
                Result = 0,
                Data = await retailerOrderQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/completed/bydelivery")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> GetsCompletedByDelivery(DateTime from, DateTime to, int distributionId)
        {
            string conditon = $@"(o.status_id in ({((int)RetailerOrderStatuses.Canceled)}, {((int)RetailerOrderStatuses.Completed)})) and o.buying_date >= '{from.Date.ToString("yyyyMMddHHmmss")}'
                                  and o.buying_date < '{to.Date.AddSeconds(86399).ToString("yyyyMMddHHmmss")}'";
            var distributions = await WebHelper.HttpGet<IEnumerable<DistributionViewModel>>(GlobalConfiguration.APIGateWayURI, AppUrl.GetDistributionsByUser, LoginSession.AccessToken);
            if (!distributions.Any(d => d.Id == distributionId))
            {
                throw new NotPermissionException();
            }
            conditon += " and o.distribution_id_to = " + distributionId;

            return new APIResult()
            {
                Result = 0,
                Data = await retailerOrderQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/bydelivery")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> GetsByDelivery(int distributionId, int? tripId = null)
        {
            var distributions = await WebHelper.HttpGet<IEnumerable<DistributionViewModel>>(GlobalConfiguration.APIGateWayURI, AppUrl.GetDistributionsByUser, LoginSession.AccessToken);
            if(!distributions.Any(d => d.Id == distributionId))
            {
                throw new NotPermissionException();
            }

            string cmd = $"o.distribution_id_to = {distributionId}";
            if(tripId != null && tripId.Value > 0)
            {
                cmd += $" AND o.trip_id = {tripId}";
            }
            else
            {
                cmd += $" AND o.trip_id is null";
            }

            return new APIResult()
            {
                Result = 0,
                Data = await retailerOrderQueries.Gets(cmd)
            };
        }

        [HttpGet]
        [Route("gets/status")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> GetsStatus()
        {
            var rs = await retailerOrderStatusQueries.Gets();
            var result = new List<RetailerOrderStatusViewModel>();
            foreach (var item in rs)
            {
                var vm = CommonHelper.Mapper<Order.UI.Models.RetailerOrderStatus, RetailerOrderStatusViewModel>(item);
                vm.Name = GetCaption(item.CaptionName);
                vm.Description = GetCaption(item.CaptionDescription);

                result.Add(vm);
            }
            return new APIResult()
            {
                Result = 0,
                Data = result
            };
        }

        [HttpGet]
        [Route("gets/audit")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> GetsAudit(long orderId, bool isHaveOrder = false)
        {
            Retailer retailer = null;
            if (!LoginSession.Roles.Any(r => r == "Administrator"))
            {
                retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={LoginSession.Id}", LoginSession.AccessToken);
                if (retailer == null)
                {
                    throw new NotPermissionException();
                }
            }

            if (isHaveOrder)
            {

                string conditon = $"o.id {orderId} and o.retailer_id = {retailer.Id}";
                var order = (await retailerOrderQueries.Gets(conditon)).FirstOrDefault();
                if(order == null)
                {
                    throw new BusinessException("Order.Retailer.Order.NotExisted");
                }

                order.Audits = await retailerOrderQueries.GetAudits(orderId, retailer.Id);

                return new APIResult()
                {
                    Result = 0,
                    Data = order
                };
            }
            else
            {
                return new APIResult()
                {
                    Result = 0,
                    Data = await retailerOrderQueries.GetAudits(orderId, retailer.Id)
                };
            }
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
        [Route("delete")]
        [AuthorizeUser("Administrator,Retailer")]
        public async Task<APIResult> Delete([FromBody]DeleteCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update/status")]
        [AuthorizeUser("Administrator,DeliveryMan")]
        public async Task<APIResult> UpdateStatus([FromBody]UpdateStatusCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update/item/status")]
        [AuthorizeUser("Administrator,DeliveryMan")]
        public async Task<APIResult> UpdateItemStatus([FromBody]UpdateStatusCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update/trip")]
        [AuthorizeUser("Administrator,DeliverySupervisor")]
        public async Task<APIResult> UpdateOrderTrip([FromBody]UpdateTripCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("proccessing")]
        [AuthorizeUser("Administrator")]
        public async Task<APIResult> ProcessOrder([FromBody]ProcessCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }
    }
}
