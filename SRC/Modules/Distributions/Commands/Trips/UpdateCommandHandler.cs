using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using Distributions.UI;
using Distributions.UI.Interfaces;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Vehicles.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Distributions.Commands.Trips
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ITripRepository tripRepository = null;
        private readonly ITripQueries tripQueries = null;
        private readonly IDistributionQueries distributionQueries = null;
        public UpdateCommandHandler(ITripRepository tripRepository, ITripQueries tripQueries, IDistributionQueries distributionQueries)
        {
            this.tripRepository = tripRepository;
            this.tripQueries = tripQueries;
            this.distributionQueries = distributionQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Trip == null || request.Trip.Id == 0)
            {
                throw new BusinessException("Trip.NotExisted");
            }

            var trip = await tripQueries.Get(request.Trip.Id);
            if (trip == null)
            {
                throw new BusinessException("Router.NotExisted");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var distribution = (await this.distributionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(d => d.Id == trip.DistributionId);
            if (distribution == null)
            {
                throw new NotPermissionException();
            }

            if (trip.DeliveryManId != null)
            {
                var deliveryMan = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetEmployee}?employeeId={trip.DeliveryManId.Value}", request.LoginSession.AccessToken);
                if (deliveryMan == null)
                {
                    throw new BusinessException("Distribution.Trip.NotExistedDeliveryMan");
                }
            }
            if (trip.DriverId != null)
            {
                var driver = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetEmployee}?employeeId={trip.DriverId.Value}", request.LoginSession.AccessToken);
                if (driver == null)
                {
                    throw new BusinessException("Distribution.Trip.NotExistedDriver");
                }
            }
            if (trip.VehicleId != null)
            {
                var vehicle = await WebHelper.HttpGet<Vehicle>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetVehicle}?id={trip.VehicleId.Value}", request.LoginSession.AccessToken);
                if (vehicle == null)
                {
                    throw new BusinessException("Distribution.Trip.NotExistedVehicle");
                }
            }

            trip.RouterId = request.Trip.RouterId;
            trip.DeliveryManId = request.Trip.DeliveryManId;
            trip.DriverId = request.Trip.DriverId;
            trip.VehicleId = request.Trip.VehicleId;
            trip.CurrentLatitude = request.Trip.CurrentLatitude;
            trip.CurrentLongitude = request.Trip.CurrentLongitude;
            trip = UpdateBuild(trip, request.LoginSession);
            return await tripRepository.Update(trip);
        }
    }
}
