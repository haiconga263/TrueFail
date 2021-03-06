﻿using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.RetailerOrder
{
    public class AddCommand : BaseCommand<int>
    {
        public RetailerOrderViewModel Order { set; get; }
        public AddCommand(RetailerOrderViewModel order)
        {
            Order = order;
        }
    }
}
