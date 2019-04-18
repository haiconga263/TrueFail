using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Farmers.Models;
using MDM.UI.Farmers.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.Commands.FarmerOrder;
using Order.UI;
using Order.UI.Interfaces;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;
using Web.Helpers;

namespace Order.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class FarmerOrderController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IFarmerOrderQueries farmerOrderQueries = null;
        private readonly IFarmerOrderStatusQueries farmerOrderStatusQueries = null;
        public FarmerOrderController(IMediator mediator, IFarmerOrderQueries farmerOrderQueries, IFarmerOrderStatusQueries farmerOrderStatusQueries = null)
        {
            this.mediator = mediator;
            this.farmerOrderQueries = farmerOrderQueries;
            this.farmerOrderStatusQueries = farmerOrderStatusQueries;
        }

        [HttpGet]
        [Route("is-changed")]
        [AuthorizeUser("Farmer")]
        public async Task<APIResult> IsChanged(DateTime? lastRequest)
        {
            var farmer = await WebHelper.HttpGet<FarmerViewModel>(GlobalConfiguration.APIGateWayURI, AppUrl.GetFarmerByUser, LoginSession.AccessToken);
            return new APIResult()
            {
                Result = 0,
                Data = new { isChanged = await farmerOrderQueries.CheckChangedByFarmer(farmer?.Id ?? 0, lastRequest ?? DateTime.Now.AddDays(-1)) }
            };
        }

        [HttpGet]
        [Route("gets")]
        [AuthorizeUser("Administrator,Farmer,Collector,Fulfillmentor")]
        public async Task<APIResult> Gets(int farmerId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await farmerOrderQueries.Gets(farmerId)
            };
        }

        [HttpGet]
        [Route("get")]
        [AuthorizeUser("Administrator,Farmer,Collector,Fulfillmentor")]
        public async Task<APIResult> Get(long orderId)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await farmerOrderQueries.Get(orderId)
            };
        }

        #region Gets UnCompleted
        [HttpGet]
        [Route("gets/un-completed")]
        [AuthorizeUser("Administrator,Farmer,Collector,Fulfillmentor")]
        public async Task<APIResult> GetsUnCompleted(int farmerId = 0)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator") && farmerId == 0)
            {
                throw new NotPermissionException();
            }
            string conditon = $"o.status_id <> {(int)FarmerOrderStatuses.Canceled} and o.status_id <> {(int)FarmerOrderStatuses.Completed}";
            if (farmerId != 0)
            {
                conditon += " and o.farmer_id = " + farmerId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await farmerOrderQueries.Gets(conditon)
            };
        }


        [HttpGet]
        [Route("gets/un-completed/by-collection")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> GetsUnCompletedByCollection(int collectionId = 0)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator") && collectionId == 0)
            {
                throw new NotPermissionException();
            }
            string conditon = $"o.status_id <> {(int)FarmerOrderStatuses.Canceled} and o.status_id <> {(int)FarmerOrderStatuses.Completed}";
            if (collectionId != 0)
            {
                conditon += " and o.collection_id = " + collectionId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await farmerOrderQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/un-completed/by-farmer")]
        [AuthorizeUser("Farmer")]
        public async Task<APIResult> GetsUnCompletedByFarmer()
        {
            var farmer = await WebHelper.HttpGet<Farmer>(GlobalConfiguration.APIGateWayURI, AppUrl.GetFarmerByUser, LoginSession.AccessToken);

            if (farmer == null || farmer.Id == 0)
                throw new BusinessException("Farmer.NotExisted");

            string conditon = $@"o.status_id <> {(int)FarmerOrderStatuses.Canceled} and o.status_id <> {(int)FarmerOrderStatuses.Completed}
                                   and o.farmer_id = {farmer.Id}";

            return new APIResult()
            {
                Result = 0,
                Data = await farmerOrderQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/completed/by-collection")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> GetsCompletedByCollection(DateTime from, DateTime to, int collectionId = 0)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator") && collectionId == 0)
            {
                throw new NotPermissionException();
            }
            string conditon = $"(o.status_id in ({((int)FarmerOrderStatuses.Canceled)}, {((int)FarmerOrderStatuses.Completed)})) and o.modified_date >= '{from.Date.ToString("yyyyMMddHHmmss")}' and o.modified_date < '{to.Date.AddSeconds(86399).ToString("yyyyMMddHHmmss")}'";
            if (collectionId != 0)
            {
                conditon += " and o.collection_id = " + collectionId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await farmerOrderQueries.Gets(conditon)
            };
        }
        #endregion


        [HttpGet]
        [Route("gets/completed")]
        [AuthorizeUser("Administrator,Farmer,Collector,Fulfillmentor")]
        public async Task<APIResult> GetsCompleted(DateTime from, DateTime to, int farmerId = 0)
        {
            if (!LoginSession.Roles.Any(r => r == "Administrator") && farmerId == 0)
            {
                throw new NotPermissionException();
            }
            string conditon = $@"(o.status_id in ({((int)FarmerOrderStatuses.Canceled)}, {((int)FarmerOrderStatuses.Completed)})) and o.buying_date >= '{from.Date.ToString("yyyyMMddHHmmss")}'
                                  and o.buying_date < '{to.Date.AddSeconds(86399).ToString("yyyyMMddHHmmss")}'";
            if (farmerId != 0)
            {
                conditon += " and o.farmer_id = " + farmerId;
            }
            return new APIResult()
            {
                Result = 0,
                Data = await farmerOrderQueries.Gets(conditon)
            };
        }

        [HttpGet]
        [Route("gets/status")]
        [AuthorizeUser("Administrator,Retailer,Collector,Fulfillmentor")]
        public async Task<APIResult> GetsStatus()
        {
            var rs = await farmerOrderStatusQueries.Gets();
            var result = new List<FarmerOrderStatusViewModel>();
            foreach (var item in rs)
            {
                var vm = CommonHelper.Mapper<Order.UI.Models.FarmerOrderStatus, FarmerOrderStatusViewModel>(item);
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

        [HttpPost]
        [Route("add")]
        [AuthorizeUser("Administrator,Farmer")]
        public async Task<APIResult> Add([FromBody]AddCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update")]
        [AuthorizeUser("Administrator,Farmer")]
        public async Task<APIResult> Update([FromBody]UpdateCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("delete")]
        [AuthorizeUser("Administrator,Farmer")]
        public async Task<APIResult> Delete([FromBody]DeleteCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update/status")]
        [AuthorizeUser("Administrator,Farmer")]
        public async Task<APIResult> UpdateStatus([FromBody]UpdateCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("update/in-collector")]
        [AuthorizeUser("Administrator,Collector")]
        public async Task<APIResult> UpdateInCollector([FromBody]UpdateInCollectorCommand command)
        {
            return new APIResult()
            {
                Result = await mediator.Send(command)
            };
        }
    }
}
