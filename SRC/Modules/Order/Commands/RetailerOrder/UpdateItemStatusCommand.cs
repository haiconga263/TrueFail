using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.RetailerOrder
{
    public class UpdateItemStatusCommand : BaseCommand<int>
    {
        public long OrderId { set; get; }
        public long OrderItemId { set; get; }
        public int StatusId { set; get; }
        public UpdateItemStatusCommand(long orderId, long orderItemId, int statusId)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
            StatusId = statusId;
        }
    }
}
