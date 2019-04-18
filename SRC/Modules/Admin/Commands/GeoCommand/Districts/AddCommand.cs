using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Districts
{
    public class AddCommand : BaseCommand<int>
    {
        public District District { set; get; }
        public AddCommand(District district)
        {
            District = district;
        }
    }
}
