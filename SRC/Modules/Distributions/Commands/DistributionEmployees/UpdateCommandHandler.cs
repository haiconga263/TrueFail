using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Distributions.Commands.DistributionEmployees
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IDistributionEmployeeRepository distributionEmployeeRepository = null;
        private readonly IDistributionEmployeeQueries distributionEmployeeQueries = null;
        public UpdateCommandHandler(IDistributionEmployeeRepository distributionEmployeeRepository, IDistributionEmployeeQueries distributionEmployeeQueries)
        {
            this.distributionEmployeeRepository = distributionEmployeeRepository;
            this.distributionEmployeeQueries = distributionEmployeeQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Employee == null || request.Employee.Id == 0 || request.Employee.EmployeeId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var distributionEmployee = (await distributionEmployeeQueries.Gets($"id = {request.Employee.Id}")).FirstOrDefault();
            if(distributionEmployee != null)
            {
                distributionEmployee.EmployeeId = request.Employee.EmployeeId;
                var rs = await distributionEmployeeRepository.Update(distributionEmployee);
                return rs == 0 ? 0 : throw new BusinessException("Common.UpdateFail");
            }

            // DistributionEmployee isn't exsited. Of course, we don't need update it.
            return 0;
        }
    }
}
