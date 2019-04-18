using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Regions
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int RegionId { set; get; }
        public DeleteCommand(int regionId)
        {
            RegionId = regionId;
        }
    }
}
