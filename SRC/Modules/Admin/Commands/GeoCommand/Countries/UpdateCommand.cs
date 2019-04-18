using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Countries
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Country Country { set; get; }
        public UpdateCommand(Country country)
        {
            Country = country;
        }
    }
}
