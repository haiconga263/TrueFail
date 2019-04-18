using System;
using System.Collections.Generic;
using System.Text;

namespace Distributions.UI.ViewModels
{
    public class PickedItemViewModel
    {
        public int ProductId { set; get; }
        public int UoMId { set; get; }
        public int Quantity { set; get; }
        public int PickedQuantity { set; get; }
    }
}
