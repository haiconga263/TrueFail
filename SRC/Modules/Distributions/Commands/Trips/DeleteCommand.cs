using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Distributions.Commands.Trips
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int TripId { set; get; }
        public DeleteCommand(int tripId)
        {
            TripId = tripId;
        }
    }
}
