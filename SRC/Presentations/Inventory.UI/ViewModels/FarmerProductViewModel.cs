using MDM.UI.Farmers.Models;
using MDM.UI.Products.Models;
using MDM.UI.UoMs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.UI.ViewModels
{
    public class FarmerProductViewModel
    {
        public int FarmerId { set; get; }
        public Farmer Farmer { set; get; }
        public int ProductId { set; get; }
        public int UoMId { set; get; }
        public Product Product { set; get; }
        public UoM UoM { set; get; }
        public int Quantity { set; get; }
        public DateTime EffectivedDate { set; get; }
    }
}
