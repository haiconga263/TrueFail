using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Provinces
{
    public class AddCommand : BaseCommand<int>
    {
        public Province Province { set; get; }
        public AddCommand(Province province)
        {
            Province = province;
        }
    }
}
