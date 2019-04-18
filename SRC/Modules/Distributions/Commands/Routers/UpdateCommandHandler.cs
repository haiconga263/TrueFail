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
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Distributions.Commands.Routers
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IRouterRepository routerRepository = null;
        private readonly IRouterQueries routerQueries = null;
        private readonly IDistributionQueries distributionQueries = null;
        public UpdateCommandHandler(IRouterRepository routerRepository, IRouterQueries routerQueries, IDistributionQueries distributionQueries)
        {
            this.routerRepository = routerRepository;
            this.routerQueries = routerQueries;
            this.distributionQueries = distributionQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Router == null || request.Router.Id == 0 || string.IsNullOrEmpty(request.Router.Name))
            {
                throw new BusinessException("Router.NotExisted");
            }

            var router = await routerQueries.Get(request.Router.Id);
            if (router == null)
            {
                throw new BusinessException("Router.NotExisted");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var distribution = (await this.distributionQueries.GetsBySupervisor(employee.Id)).FirstOrDefault(d => d.Id == router.DistributionId);
            if (distribution == null)
            {
                throw new NotPermissionException();
            }

            router.Name = request.Router.Name;
            router.CurrentLatitude = request.Router.CurrentLatitude;
            router.CurrentLongitude = request.Router.CurrentLongitude;
            router.Radius = request.Router.Radius;
            router.CountryId = request.Router.CountryId;
            router.ProvinceId = request.Router.ProvinceId;
            router = UpdateBuild(router, request.LoginSession);
            return await routerRepository.Update(router);
        }
    }
}
