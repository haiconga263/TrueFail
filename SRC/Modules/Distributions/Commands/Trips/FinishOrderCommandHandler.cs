using Common;
using Common.Exceptions;
using Distributions.UI;
using Distributions.UI.Interfaces;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
using Order.UI;
using Order.UI.Models;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Distributions.Commands.Trips
{
    public class FinishOrderCommandHandler : BaseCommandHandler<FinishOrderCommand, int>
    {
        private readonly ITripRepository tripRepository = null;
        private readonly ITripQueries tripQueries = null;
        private readonly IDistributionQueries distributionQueries = null;
        public FinishOrderCommandHandler(ITripRepository tripRepository, ITripQueries tripQueries, IDistributionQueries distributionQueries)
        {
            this.tripRepository = tripRepository;
            this.tripQueries = tripQueries;
            this.distributionQueries = distributionQueries;
        }
        public override async Task<int> HandleCommand(FinishOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderId == 0)
            {
                throw new BusinessException("Order.NotExisted");
            }

            var order = await WebHelper.HttpGet<RetailerOrderViewModel>(GlobalConfiguration.APIGateWayURI, $"{UI.AppUrl.GetRetailerOrder}?orderId={request.OrderId}", request.LoginSession.AccessToken);
            if(order == null)
            {
                throw new BusinessException("Order.NotExisted");
            }

            if(order.StatusId != (int)RetailerOrderStatuses.InLogistic)
            {
                throw new BusinessException("Order.WrongStep");
            }

            #warning Logic here
            //logic here


            await WebHelper.HttpPost<object>(GlobalConfiguration.APIGateWayURI, UI.AppUrl.UpdateRetailerOrderStatus, new { request.OrderId, StatusId = (int)RetailerOrderStatuses.Completed });

            try
            {
                await tripRepository.CreateTripAudit(new UI.Models.TripAudit()
                {
                    TripId = order.TripId == null ? 0 : order.TripId.Value,
                    StatusId = (int)TripStatuses.OnTriped,
                    Latitude = request.Latitude == null ? 0 : request.Latitude.Value,
                    Longitude = request.Longitude == null ? 0 : request.Longitude.Value
                });
            }
            catch { }

            return 0;
        }
    }
}
