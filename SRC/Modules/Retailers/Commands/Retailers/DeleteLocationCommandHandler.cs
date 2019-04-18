using MDM.UI.Farmers.Interfaces;
using MDM.UI.Farmers.Models;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.Interfaces;
using MDM.UI.Retailers.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Retailers.Commands.Commands.Retailers
{
    public class DeleteLocationCommandHandler : BaseCommandHandler<DeleteLocationCommand, int>
    {
        private readonly IRetailerRepository retailerRepository = null;
        public DeleteLocationCommandHandler(IRetailerRepository retailerRepository)
        {
            this.retailerRepository = retailerRepository;
        }
        public override async Task<int> HandleCommand(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            return await retailerRepository.DeleteLocation(DeleteBuild(new RetailerLocation()
            {
                Id = request.LocationId
            }, request.LoginSession));
        }
    }
}
