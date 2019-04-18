using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Regions
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IRegionRepository regionRepository = null;
        public DeleteCommandHandler(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await regionRepository.Delete(DeleteBuild(new Region()
            {
                Id = request.RegionId
            }, request.LoginSession));
        }
    }
}
