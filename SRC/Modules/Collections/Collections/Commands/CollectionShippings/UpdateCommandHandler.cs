using Collections.UI;
using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Common.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Products.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Collections.Commands.CollectionShippings
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ICFShippingRepository cFShippingRepository = null;
        private readonly ICFShippingQueries cFShippingQueries = null;
        private readonly ICollectionQueries collectionQueries = null;
        public UpdateCommandHandler(ICFShippingRepository cFShippingRepository, ICFShippingQueries cFShippingQueries, ICollectionQueries collectionQueries)
        {
            this.cFShippingRepository = cFShippingRepository;
            this.collectionQueries = collectionQueries;
            this.cFShippingQueries = cFShippingQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Shipping == null || request.Shipping.Id == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var shipping = await cFShippingQueries.Get(request.Shipping.Id);
            if(shipping == null)
            {
                throw new BusinessException("Trip.NotExisted");
            }

            if (shipping.StatusId != (int)Distributions.UI.TripStatuses.Created)
            {
                throw new BusinessException("Common.ErrorWithStep");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var collection = (await collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(c => c.Id == shipping.CollectionId);
            if (collection == null)
            {
                throw new NotPermissionException();
            }

            shipping = UpdateBuild(shipping, request.LoginSession);
            shipping.FulfillmentId = request.Shipping.FulfillmentId;
            shipping.VehicleId = request.Shipping.VehicleId;
            shipping.ShipperId = request.Shipping.ShipperId;
            shipping.DeliveryDate = request.Shipping.DeliveryDate;
            return await cFShippingRepository.Update(shipping);
        }
    }
}
