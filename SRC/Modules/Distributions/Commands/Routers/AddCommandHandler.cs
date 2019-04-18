using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using Distributions.UI;
using Distributions.UI.Interfaces;
using MDM.UI;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Distributions.Commands.Routers
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IRouterRepository routerRepository = null;
        private readonly IDistributionQueries distributionQueries = null;
        public AddCommandHandler(IRouterRepository routerRepository, IDistributionQueries distributionQueries)
        {
            this.routerRepository = routerRepository;
            this.distributionQueries = distributionQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {

            if (request.Router == null || string.IsNullOrEmpty(request.Router.Name) || request.Router.DistributionId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var employee = await WebHelper.HttpGet<Employee>(GlobalConfiguration.APIGateWayURI, AppUrl.GetEmployeeByUser, request.LoginSession.AccessToken);
            if (employee == null)
            {
                throw new NotPermissionException();
            }
            var distribution = (await this.distributionQueries.GetsBySupervisor(employee.Id)).FirstOrDefault(d => d.Id == request.Router.DistributionId);
            if (distribution == null)
            {
                throw new NotPermissionException();
            }

            request.Router.DistributionId = distribution.Id;
            request.Router = CreateBuild(request.Router, request.LoginSession);
            return (await routerRepository.Create(request.Router)) > 0 ? 0 : -1;
        }
    }
}
