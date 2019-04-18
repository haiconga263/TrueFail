using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Farmers.Models;
using MDM.UI.Farmers.ViewModels;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.ViewModels;
using Web.Controllers;

namespace Retailers.Commands.Commands.Retailers
{
    public class UpdateLocationCommand : BaseCommand<int>
    {
        public RetailerLocationViewModel Location { set; get; }
        public UpdateLocationCommand(RetailerLocationViewModel location)
        {
            Location = location;
        }
    }
}
