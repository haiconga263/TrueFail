using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Regions
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Region Region { set; get; }
        public UpdateCommand(Region region)
        {
            Region = region;
        }
    }
}
