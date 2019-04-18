using MDM.UI.Collections.ViewModels;
using MDM.UI.Common.Models;
using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Products.ViewModels;
using System.Collections.Generic;
using Web.Controllers;

namespace Collections.Commands.CollectionShippings
{
    public class UpdateItemCommand : BaseCommand<int>
    {
        public long ShippingId { set; get; }
        public List<CFShippingItem> Items { set; get; }
        public UpdateItemCommand(long shippingId, List<CFShippingItem> items)
        {
            ShippingId = shippingId;
            Items = items;
        }
    }
}
