using Common;
using Common.Exceptions;
using Distributions.UI;
using Distributions.UI.Interfaces;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
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
    public class UpdateStatusCommandHandler : BaseCommandHandler<UpdateStatusCommand, int>
    {
        private readonly ITripRepository tripRepository = null;
        private readonly ITripQueries tripQueries = null;
        private readonly IDistributionQueries distributionQueries = null;
        public UpdateStatusCommandHandler(ITripRepository tripRepository, ITripQueries tripQueries, IDistributionQueries distributionQueries)
        {
            this.tripRepository = tripRepository;
            this.tripQueries = tripQueries;
            this.distributionQueries = distributionQueries;
        }
        public override async Task<int> HandleCommand(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.TripId == 0)
            {
                throw new BusinessException("Trip.NotExisted");
            }

            if (!Enum.IsDefined(typeof(TripStatuses), request.StatusId))
            {
                throw new BusinessException("Trip.NotExistedStatus");
            }

            var trip = await tripQueries.Get(request.TripId);
            if (trip == null)
            {
                throw new BusinessException("Trip.NotExisted");
            }

            var status = (TripStatuses)request.StatusId;

            if(status != TripStatuses.Canceled)
            {
                if(request.StatusId != trip.StatusId + 1)
                {
                    throw new BusinessException("Trip.WrongStep");
                }
            }
            if(status == TripStatuses.Confirmed)
            {
                if(!request.LoginSession.Roles.Any(r => r == "DeliverySupervisor"))
                {
                    throw new NotPermissionException();
                }
                var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
                if (employee != null)
                {
                    var distribution = (await distributionQueries.GetsBySupervisor(employee.Id)).FirstOrDefault();
                    if (distribution != null)
                    {
                        trip.CurrentLatitude = distribution.Address.Latitude;
                        trip.CurrentLongitude = distribution.Address.Longitude;
                    }
                }

                if(trip.DeliveryManId == null)
                {
                    throw new BusinessException("Distribution.Trip.RequiredDeliveryMan");
                }
                if(trip.DriverId == null)
                {
                    throw new BusinessException("Distribution.Trip.RequiredDriver");
                }
                if (trip.VehicleId == null)
                {
                    throw new BusinessException("Distribution.Trip.RequiredVehicle");
                }

                var orders = await WebHelper.HttpGet<IEnumerable<RetailerOrderViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerOrders}?distributionId={trip.DistributionId}&tripId={trip.Id}", request.LoginSession.AccessToken);
                if(orders.Count() == 0)
                {
                    throw new BusinessException("Distribution.Trip.NotExistedOrders");
                }
            }
            else if(status == TripStatuses.OnTriped || status == TripStatuses.Finished)
            {
                trip.CurrentLongitude = request.Longitude ?? trip.CurrentLongitude;
                trip.CurrentLatitude = request.Latitude ?? trip.CurrentLatitude;
            }

            trip.StatusId = (short)request.StatusId;
            trip = UpdateBuild(trip, request.LoginSession);
            var rs = await tripRepository.Update(trip);

            try
            {
                await tripRepository.CreateTripAudit(new UI.Models.TripAudit()
                {
                    TripId = trip.Id,
                    StatusId = (int)TripStatuses.OnTriped,
                    Latitude = trip.CurrentLatitude == null ? 0 : trip.CurrentLatitude.Value,
                    Longitude = trip.CurrentLongitude == null ? 0 : trip.CurrentLongitude.Value
                });
            }
            catch { }

            return rs;
        }
    }
}
