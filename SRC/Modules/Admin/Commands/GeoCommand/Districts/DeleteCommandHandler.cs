using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Districts
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IDistrictRepository districtRepository = null;
        public DeleteCommandHandler(IDistrictRepository districtRepository)
        {
            this.districtRepository = districtRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await districtRepository.Delete(DeleteBuild(new District()
            {
                Id = request.DistrictId
            }, request.LoginSession));
        }
    }
}
