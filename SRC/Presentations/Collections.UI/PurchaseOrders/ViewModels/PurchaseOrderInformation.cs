using Collections.UI.PurchaseOrders.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.UI.PurchaseOrders.ViewModels
{
    public class PurchaseOrderInformation : PurchaseOrder
    {
        public List<PurchaseOrderItem> Items { get; set; }
    }
}
