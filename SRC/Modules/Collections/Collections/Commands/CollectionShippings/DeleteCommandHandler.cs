using Collections.UI;
using Common;
using Common.Exceptions;
using Distributions.UI;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using MDM.UI.Common.Interfaces;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;
using Web.Helpers;

namespace Collections.Commands.CollectionShippings
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly ICFShippingRepository cFShippingRepository = null;
        private readonly ICFShippingQueries cFShippingQueries = null;
        private readonly ICollectionQueries collectionQueries = null;
        public DeleteCommandHandler(ICFShippingRepository cFShippingRepository, ICFShippingQueries cFShippingQueries, ICollectionQueries collectionQueries)
        {
            this.cFShippingRepository = cFShippingRepository;
            this.collectionQueries = collectionQueries;
            this.cFShippingQueries = cFShippingQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.ShippingId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var shipping = await cFShippingQueries.Get(request.ShippingId);
            if (shipping == null)
            {
                throw new BusinessException("Trip.NotExisted");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, UI.AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var collection = (await collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(c => c.Id == shipping.CollectionId);
            if (collection == null)
            {
                throw new NotPermissionException();
            }

            if(shipping.StatusId != (int)TripStatuses.Created)
            {
                throw new BusinessException("Common.ErrorWithStep");
            }

            shipping = UpdateBuild(shipping, request.LoginSession);
            return await cFShippingRepository.Delete(shipping);
        }
    }
}
