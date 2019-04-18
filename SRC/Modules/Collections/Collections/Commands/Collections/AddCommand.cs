using MDM.UI.Collections.ViewModels;
using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Collections.Commands.Collections
{
    public class AddCommand : BaseCommand<int>
    {
        public CollectionViewModel Collection { set; get; }
        public AddCommand(CollectionViewModel collection)
        {
            Collection = collection;
        }
    }
}
