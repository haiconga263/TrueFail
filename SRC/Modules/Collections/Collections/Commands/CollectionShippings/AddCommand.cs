using MDM.UI.Collections.ViewModels;
using MDM.UI.Common.Models;
using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Collections.Commands.CollectionShippings
{
    public class AddCommand : BaseCommand<int>
    {
        public CFShipping Shipping { set; get; }
        public AddCommand(CFShipping shipping)
        {
            Shipping = shipping;
        }
    }
}
