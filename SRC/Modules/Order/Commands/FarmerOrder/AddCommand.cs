using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.FarmerOrder
{
    public class AddCommand : BaseCommand<int>
    {
        public FarmerOrderViewModel Order { set; get; }
        public AddCommand(FarmerOrderViewModel order)
        {
            Order = order;
        }
    }
}
