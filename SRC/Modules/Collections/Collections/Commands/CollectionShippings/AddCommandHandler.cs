using Collections.UI;
using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI;
using MDM.UI.Collections.Interfaces;
using MDM.UI.Common.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Collections.Commands.CollectionShippings
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ICFShippingRepository cFShippingRepository = null;
        private readonly ICFShippingQueries cFShippingQueries = null;
        private readonly ICollectionQueries collectionQueries = null;
        public AddCommandHandler(ICFShippingRepository cFShippingRepository, ICFShippingQueries cFShippingQueries, ICollectionQueries collectionQueries)
        {
            this.cFShippingRepository = cFShippingRepository;
            this.collectionQueries = collectionQueries;
            this.cFShippingQueries = cFShippingQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Shipping == null || request.Shipping.CollectionId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var collection = (await collectionQueries.GetsByEmployeeId(employee.Id)).FirstOrDefault(c => c.Id == request.Shipping.CollectionId);
            if (collection == null)
            {
                throw new NotPermissionException();
            }

            request.Shipping = CreateBuild(request.Shipping, request.LoginSession);
            request.Shipping.Code = await cFShippingQueries.GenarateCode();
            request.Shipping.StatusId = (int)Distributions.UI.TripStatuses.Created;
            return (await cFShippingRepository.Add(request.Shipping) > 0) ? 0 : -1;
        }
    }
}
