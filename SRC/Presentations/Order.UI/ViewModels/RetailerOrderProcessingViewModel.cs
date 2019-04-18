using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class RetailerOrderProcessingViewModel
    {
        public long OrderId { set; get; }
        public IEnumerable<RetailerOrderItemProcessingViewModel> Items { set; get; }
    }
}
