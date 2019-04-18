using MDM.UI.Farmers.Interfaces;
using MDM.UI.Farmers.Models;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Famrers.Commands.Commands.Famrers
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IFarmerRepository farmerRepository = null;
        public DeleteCommandHandler(IFarmerRepository farmerRepository)
        {
            this.farmerRepository = farmerRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await farmerRepository.Delete(DeleteBuild(new Farmer()
            {
                Id = request.FarmerId
            }, request.LoginSession));
        }
    }
}
