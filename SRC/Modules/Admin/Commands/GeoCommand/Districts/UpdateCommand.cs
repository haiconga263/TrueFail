using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Districts
{
    public class UpdateCommand : BaseCommand<int>
    {
        public District District { set; get; }
        public UpdateCommand(District district)
        {
            District = district;
        }
    }
}
