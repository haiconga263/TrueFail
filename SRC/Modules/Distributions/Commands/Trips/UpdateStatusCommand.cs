using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Distributions.Commands.Trips
{
    public class UpdateStatusCommand : BaseCommand<int>
    {
        public int TripId { set; get; }
        public int StatusId { set; get; }

        public double? Longitude { set; get; }
        public double? Latitude { set; get; }
        public UpdateStatusCommand(int tripId, int statusId)
        {
            TripId = tripId;
            StatusId = statusId;
        }
    }
}
