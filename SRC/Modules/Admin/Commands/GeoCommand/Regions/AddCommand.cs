using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Regions
{
    public class AddCommand : BaseCommand<int>
    {
        public Region Region { set; get; }
        public AddCommand(Region region)
        {
            Region = region;
        }
    }
}
