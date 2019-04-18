using Common;
using Common.Exceptions;
using Distributions.Commands.Trips;
using Distributions.UI;
using Distributions.UI.Interfaces;
using Distributions.UI.ViewModels;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Web.Helpers;

namespace Distributions.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class TripController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ITripQueries tripQueries = null;
        private readonly IDistributionQueries distributionQueries = null;
        public TripController(IMediator mediator, ITripQueries tripQueries, IDistributionQueries distributionQueries)
        {
            this.mediator = mediator;
            this.tripQueries = tripQueries;
            this.distributionQueries = distributionQueries;
        }

        #region Manager

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> Gets(int distributionId)
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var distributions = (await this.distributionQueries.GetsBySupervisor(employee.Id)).FirstOrDefault(d => d.Id == distributionId); ;
            if (distributions == null)
            {
                throw new NotPermissionException();
            }
            return new APIResult()
            {
                Result = 0,
                Data = await tripQueries.Gets($"t.distribution_id = {distributionId}")
            };
        }

        [HttpGet]
        [Route("gets/history")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> GetHistorys(int distributionId, DateTime from, DateTime to)
        {
            if (from.Date > to.Date)
                throw new BusinessException("Common.SearchRangeDateWrong");
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
            string cmd = $"t.distribution_id = {distribution.Id}";
            cmd += $" and t.created_date >= '{from.Date.ToString("yyyyMMdd")}' and t.created_date < {to.Date.AddSeconds(86399).ToString("yyyyMMdd")}";
            return new APIResult()
            {
                Result = 0,
                Data = await tripQueries.GetHistorys(cmd)
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
                Data = await tripQueries.Get(id)
            };
        }

        [HttpGet]
        [Route("gets/status")]
        [AuthorizeUser("DeliverySupervisor,DeliveryMan,DeliveryDriver")]
        public async Task<APIResult> GetStatuses()
        {
            return new APIResult()
            {
                Result = 0,
                Data = await tripQueries.GetStatuses()
            };
        }

        [HttpGet]
        [Route("gets/order")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> GetOrders(int distributionId, int? tripId)
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
            var orders = await WebHelper.HttpGet<IEnumerable<RetailerOrderViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerOrders}?distributionId={distribution.Id}&tripId={tripId}", LoginSession.AccessToken);
            return new APIResult()
            {
                Result = 0,
                Data = orders
            };
        }

        [HttpGet]
        [Route("gets/order/history")]
        [AuthorizeUser("DeliverySupervisor")]
        public async Task<APIResult> GetOrderHistorys(int distributionId, DateTime from, DateTime to)
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
            var orders = await WebHelper.HttpGet<IEnumerable<RetailerOrderViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetOrderHistorys}?distributionId={distribution.Id}&from={from}&to={to}", LoginSession.AccessToken);
            return new APIResult()
            {
                Result = 0,
                Data = orders
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

        [HttpPost]
        [Route("update/status")]
        [AuthorizeUser("DeliverySupervisor,DeliveryMan")]
        public async Task<APIResult> UpdateStatus([FromBody]UpdateStatusCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        #endregion

        #region Processing

        [HttpGet]
        [Route("get/current/by-deliveryman")]
        [AuthorizeUser("DeliveryMan")]
        public async Task<APIResult> GetCurrentTrip()
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }

            return new APIResult()
            {
                Result = 0,
                Data = (await tripQueries.GetByDeliveryMan(employee.Id)).FirstOrDefault()
            };
        }

        [HttpGet]
        [Route("gets/order/by-deliveryman")]
        [AuthorizeUser("DeliveryMan")]
        public async Task<APIResult> GetOrders(int tripId)
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }

            var trip = await tripQueries.Get(tripId);
            if(trip == null || trip.DeliveryManId == employee.Id)
            {
                throw new BusinessException("Trip.NotExsited");
            }

            var orders = await WebHelper.HttpGet<IEnumerable<RetailerOrderViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerOrders}?distributionId={trip.DistributionId}&tripId={trip.Id}", LoginSession.AccessToken);

            return new APIResult()
            {
                Result = 0,
                Data = orders
            };
        }

        [HttpGet]
        [Route("gets/picked-list-goods/by-deliveryman")]
        [AuthorizeUser("DeliveryMan")]
        public async Task<APIResult> GetPickedListGoods(int tripId)
        {
            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }

            var trip = await tripQueries.Get(tripId);
            if (trip == null || trip.DeliveryManId == employee.Id)
            {
                throw new BusinessException("Trip.NotExsited");
            }

            var orders = await WebHelper.HttpGet<IEnumerable<RetailerOrderViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerOrders}?distributionId={trip.DistributionId}&tripId={trip.Id}", LoginSession.AccessToken);

            var pickedList = new List<PickedItemViewModel>();
            foreach (var order in orders)
            {
                foreach (var item in order.Items)
                {
                    var goods = pickedList.FirstOrDefault(i => i.ProductId == item.ProductId && i.UoMId == item.UoMId);
                    if(goods == null)
                    {
                        goods = new PickedItemViewModel()
                        {
                            ProductId = item.ProductId,
                            UoMId = item.UoMId,
                            Quantity = item.OrderedQuantity
                        };
                        pickedList.Add(goods);
                    }
                    else
                    {
                        goods.Quantity += item.OrderedQuantity;
                    }
                }
            }

            return new APIResult()
            {
                Result = 0,
                Data = pickedList
            };
        }

        [HttpPost]
        [Route("update/finish-order/by-deliveryman")]
        [AuthorizeUser("DeliveryMan")]
        public async Task<APIResult> FinishOrder([FromBody]FinishOrderCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("audit")]
        [AuthorizeUser("DeliveryMan")]
        public async Task<APIResult> Autdit([FromBody]TripAuditCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        #endregion
    }
}
