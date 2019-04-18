using MDM.UI.Distributions.Interfaces;
using MDM.UI.Distributions.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Distributions.Commands.DistributionEmployees
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IDistributionEmployeeRepository distributionEmployeeRepository = null;
        public DeleteCommandHandler(IDistributionEmployeeRepository distributionEmployeeRepository)
        {
            this.distributionEmployeeRepository = distributionEmployeeRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await distributionEmployeeRepository.Delete(request.EmployeeId);
        }
    }
}
