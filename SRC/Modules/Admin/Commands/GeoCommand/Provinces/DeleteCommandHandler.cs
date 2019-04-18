using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Provinces
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IProvinceRepository provinceRepository = null;
        public DeleteCommandHandler(IProvinceRepository provinceRepository)
        {
            this.provinceRepository = provinceRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await provinceRepository.Delete(DeleteBuild(new Province()
            {
                Id = request.ProvinceId
            }, request.LoginSession));
        }
    }
}
