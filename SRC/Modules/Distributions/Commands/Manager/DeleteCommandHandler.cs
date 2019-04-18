using MDM.UI.Distributions.Interfaces;
using MDM.UI.Distributions.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Distributions.Commands.Manager
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IDistributionRepository distributionRepository = null;
        public DeleteCommandHandler(IDistributionRepository distributionRepository)
        {
            this.distributionRepository = distributionRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await distributionRepository.Delete(UpdateBuild(new Distribution()
            {
                Id = request.DistributionId
            }, request.LoginSession));
        }
    }
}
