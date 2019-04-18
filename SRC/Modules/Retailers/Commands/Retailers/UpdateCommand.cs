using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Farmers.Models;
using MDM.UI.Farmers.ViewModels;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.ViewModels;
using Web.Controllers;

namespace Retailers.Commands.Commands.Retailers
{
    public class UpdateCommand : BaseCommand<int>
    {
        public RetailerViewModel Retailer { set; get; }
        public UpdateCommand(RetailerViewModel retailer)
        {
            Retailer = retailer;
        }
    }
}
