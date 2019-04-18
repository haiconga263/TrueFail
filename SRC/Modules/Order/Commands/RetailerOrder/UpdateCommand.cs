using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.RetailerOrder
{
    public class UpdateCommand : BaseCommand<int>
    {
        public RetailerOrderViewModel Order { set; get; }
        public UpdateCommand(RetailerOrderViewModel order)
        {
            Order = order;
        }
    }
}
