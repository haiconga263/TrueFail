using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.FarmerOrder
{
    public class DeleteCommand : BaseCommand<int>
    {
        public long OrderId { set; get; }
        public DeleteCommand(long orderId)
        {
            OrderId = orderId;
        }
    }
}
