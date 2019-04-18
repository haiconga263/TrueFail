using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Vehicles.ViewModels;
using Web.Controllers;

namespace Admin.Commands.VehicleCommand
{
    public class UpdateCommand : BaseCommand<int>
    {
        public VehicleViewModel Vehicle { set; get; }
        public UpdateCommand(VehicleViewModel vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
