using MDM.UI.Vehicles.Interfaces;
using MDM.UI.Vehicles.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.VehicleCommand
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IVehicleRepository vehicleRepository = null;
        public DeleteCommandHandler(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await vehicleRepository.Delete(DeleteBuild(new Vehicle()
            {
                Id = request.VehicleId
            }, request.LoginSession));
        }
    }
}
