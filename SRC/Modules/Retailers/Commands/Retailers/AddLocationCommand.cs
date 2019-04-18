using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Farmers.Models;
using MDM.UI.Farmers.ViewModels;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Retailers.Commands.Commands.Retailers
{
    public class AddLocationCommand : BaseCommand<int>
    {
        public RetailerLocationViewModel Location { set; get; }
        public AddLocationCommand(RetailerLocationViewModel location)
        {
            Location = location;
        }
    }
}
