using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.RetailerOrder
{
    public class UpdateTripCommand : BaseCommand<int>
    {
        public long OrderId { set; get; }
        public int TripId { set; get; }
        public UpdateTripCommand(long orderId, int tripId)
        {
            OrderId = orderId;
            TripId = tripId;
        }
    }
}
