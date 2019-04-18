using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.VehicleCommand
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int VehicleId { set; get; }
        public DeleteCommand(int vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}
