using Distributions.UI.Interfaces;
using Distributions.UI.Models;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Distributions.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Distributions.Commands.Trips
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly ITripRepository tripRepository = null;
        public DeleteCommandHandler(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await tripRepository.Delete(UpdateBuild(new Trip()
            {
                Id = request.TripId
            }, request.LoginSession));
        }
    }
}
