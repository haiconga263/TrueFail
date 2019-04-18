using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Vehicles.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.VehicleCommand
{
    public class AddCommand : BaseCommand<int>
    {
        public VehicleViewModel Vehicle { set; get; }
        public AddCommand(VehicleViewModel vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
