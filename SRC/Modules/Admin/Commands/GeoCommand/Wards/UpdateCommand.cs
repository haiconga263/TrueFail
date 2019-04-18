using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Wards
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Ward Ward { set; get; }
        public UpdateCommand(Ward ward)
        {
            Ward = ward;
        }
    }
}
