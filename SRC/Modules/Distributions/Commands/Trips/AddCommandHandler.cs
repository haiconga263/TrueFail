using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using Distributions.UI;
using Distributions.UI.Interfaces;
using MDM.UI;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Vehicles.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Distributions.Commands.Trips
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ITripRepository tripRepository = null;
        private readonly ITripQueries tripQueries = null;
        private readonly IDistributionQueries distributionQueries = null;
        public AddCommandHandler(ITripRepository tripRepository, ITripQueries tripQueries, IDistributionQueries distributionQueries)
        {
            this.tripRepository = tripRepository;
            this.tripQueries = tripQueries;
            this.distributionQueries = distributionQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {

            if (request.Trip == null || request.Trip.DistributionId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var distribution = (await this.distributionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(d => d.Id == request.Trip.DistributionId);
            if (distribution == null)
            {
                throw new NotPermissionException();
            }

            if (request.Trip.DeliveryManId != null)
            {
                var deliveryMan = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetEmployee}?employeeId={request.Trip.DeliveryManId.Value}", request.LoginSession.AccessToken);
                if (deliveryMan == null)
                {
                    throw new BusinessException("Distribution.Trip.NotExistedDeliveryMan");
                }
            }
            if (request.Trip.DriverId != null)
            {
                var driver = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetEmployee}?employeeId={request.Trip.DriverId.Value}", request.LoginSession.AccessToken);
                if (driver == null)
                {
                    throw new BusinessException("Distribution.Trip.NotExistedDriver");
                }
            }
            if (request.Trip.VehicleId != null)
            {
                var vehicle = await WebHelper.HttpGet<Vehicle>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetVehicle}?id={request.Trip.VehicleId.Value}", request.LoginSession.AccessToken);
                if (vehicle == null)
                {
                    throw new BusinessException("Distribution.Trip.NotExistedVehicle");
                }
            }

            request.Trip.DistributionId = distribution.Id;
            request.Trip.Code = await tripQueries.GenarateCode();
            request.Trip.StatusId = (int)TripStatuses.Created;
            request.Trip = CreateBuild(request.Trip, request.LoginSession);
            return (await tripRepository.Create(request.Trip)) > 0 ? 0 : -1;
        }
    }
}
