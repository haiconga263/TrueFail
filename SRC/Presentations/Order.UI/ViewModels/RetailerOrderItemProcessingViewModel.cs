using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class RetailerOrderItemProcessingViewModel
    {
        public long OrderItemId { set; get; }
        public IEnumerable<RetailerOrderPlanningProcessingViewModel> Plannings { set; get; }
    }
}
