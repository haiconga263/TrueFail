using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Provinces
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Province Province { set; get; }
        public UpdateCommand(Province province)
        {
            Province = province;
        }
    }
}
