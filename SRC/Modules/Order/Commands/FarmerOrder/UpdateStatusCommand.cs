using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.FarmerOrder
{
    public class UpdateStatusCommand : BaseCommand<int>
    {
        public long OrderId { set; get; }
        public int StatusId { set; get; }
        public UpdateStatusCommand(long orderId, int statusId)
        {
            OrderId = orderId;
            StatusId = statusId;
        }
    }
}
