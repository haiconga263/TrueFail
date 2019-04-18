using Collections.UI.POActivities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.UI.PurchaseOrders.ViewModels
{
    public class PurchaseOrderChangedInfomation
    {
        public bool IsChanged { get; set; }
        public DateTime LastChanged { get; set; }
        public IEnumerable<POActivity> Activities { get; set; }
    }
}
