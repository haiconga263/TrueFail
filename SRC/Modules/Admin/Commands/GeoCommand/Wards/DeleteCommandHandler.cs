using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Wards
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IWardRepository wardRepository = null;
        public DeleteCommandHandler(IWardRepository wardRepository)
        {
            this.wardRepository = wardRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await wardRepository.Delete(DeleteBuild(new Ward()
            {
                Id = request.WardId
            }, request.LoginSession));
        }
    }
}
