using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Wards
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int WardId { set; get; }
        public DeleteCommand(int wardId)
        {
            WardId = wardId;
        }
    }
}
