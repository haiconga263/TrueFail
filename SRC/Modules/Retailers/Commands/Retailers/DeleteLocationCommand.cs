using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Retailers.Commands.Commands.Retailers
{
    public class DeleteLocationCommand : BaseCommand<int>
    {
        public int LocationId { set; get; }
        public DeleteLocationCommand(int locationId)
        {
            LocationId = locationId;
        }
    }
}
