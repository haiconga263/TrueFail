using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.FarmerOrder
{
    public class UpdateInCollectorCommand : BaseCommand<int>
    {
        public FarmerOrderViewModel Order { set; get; }
        public UpdateInCollectorCommand(FarmerOrderViewModel order)
        {
            Order = order;
        }
    }
}
