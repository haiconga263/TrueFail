using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Farmers.Models;
using MDM.UI.Farmers.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Famrers.Commands.Commands.Famrers
{
    public class AddCommand : BaseCommand<int>
    {
        public FarmerViewModel Farmer { set; get; }
        public AddCommand(FarmerViewModel farmer)
        {
            Farmer = farmer;
        }
    }
}
