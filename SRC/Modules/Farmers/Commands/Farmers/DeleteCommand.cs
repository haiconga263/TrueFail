using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Famrers.Commands.Commands.Famrers
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int FarmerId { set; get; }
        public DeleteCommand(int farmerId)
        {
            FarmerId = farmerId;
        }
    }
}
