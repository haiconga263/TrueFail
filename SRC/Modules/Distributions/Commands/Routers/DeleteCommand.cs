using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Distributions.Commands.Routers
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int RouterId { set; get; }
        public DeleteCommand(int routerId)
        {
            RouterId = routerId;
        }
    }
}
