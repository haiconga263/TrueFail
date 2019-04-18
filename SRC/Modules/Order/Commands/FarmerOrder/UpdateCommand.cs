using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.FarmerOrder
{
    public class UpdateCommand : BaseCommand<int>
    {
        public FarmerOrderViewModel Order { set; get; }
        public UpdateCommand(FarmerOrderViewModel order)
        {
            Order = order;
        }
    }
}
