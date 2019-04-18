using MDM.UI.Collections.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Collections.ViewModels
{
    public class CollectionInventoryBySKUViewModel
    {
        public int ProductId { set; get; }
        public int UoMId { set; get; }
        public long Quantity { set; get; }

        public List<CollectionInventory> Details { set; get; } = new List<CollectionInventory>();
    }
}
