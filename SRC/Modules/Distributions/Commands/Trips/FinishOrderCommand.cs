using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Distributions.Commands.Trips
{
    public class FinishOrderCommand : BaseCommand<int>
    {
        public int OrderId { set; get; }

        public double? Longitude { set; get; }
        public double? Latitude { set; get; }
        public FinishOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
