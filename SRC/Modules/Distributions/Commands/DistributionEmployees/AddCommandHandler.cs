using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Distributions.Commands.DistributionEmployees
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IDistributionEmployeeRepository distributionEmployeeRepository = null;
        private readonly IDistributionEmployeeQueries distributionEmployeeQueries = null;
        public AddCommandHandler(IDistributionEmployeeRepository distributionEmployeeRepository, IDistributionEmployeeQueries distributionEmployeeQueries)
        {
            this.distributionEmployeeRepository = distributionEmployeeRepository;
            this.distributionEmployeeQueries = distributionEmployeeQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {

            if (request.Employee == null || request.Employee.DistributionId == 0 || request.Employee.EmployeeId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var checkEmployee = (await distributionEmployeeQueries.Gets($"distribution_id = {request.Employee.DistributionId} and employee_id = {request.Employee.EmployeeId}")).FirstOrDefault();
            if(checkEmployee != null)
            {
                throw new BusinessException("Employee.NotExisted");
            }

            return await distributionEmployeeRepository.Add(request.Employee) > 0 ? 0 : throw new BusinessException("Common.AddFail");
        }
    }
}
