using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.ProductCommand
{
    public class AddCommand : BaseCommand<int>
    {
        public ProductViewModel Product { set; get; }
        public AddCommand(ProductViewModel product)
        {
            Product = product;
        }
    }
}
