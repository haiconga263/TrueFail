using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Farmers.Models;
using MDM.UI.Farmers.ViewModels;
using MDM.UI.Geographical.Models;
using Web.Controllers;

namespace Famrers.Commands.Commands.Famrers
{
    public class UpdateCommand : BaseCommand<int>
    {
        public FarmerViewModel Farmer { set; get; }
        public UpdateCommand(FarmerViewModel farmer)
        {
            Farmer = farmer;
        }
    }
}
