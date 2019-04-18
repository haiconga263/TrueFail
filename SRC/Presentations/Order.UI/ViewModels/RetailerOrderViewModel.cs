using MDM.UI.Retailers.Models;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class RetailerOrderViewModel : RetailerOrder
    {
        public Retailer Retailer { set; get; }
        public List<RetailerOrderItemViewModel> Items { set; get; } = new List<RetailerOrderItemViewModel>();

        public IEnumerable<RetailerOrderAuditViewModel> Audits { set; get; } = null;


        public DateTime OrderedDate => CreatedDate;
        public DateTime? CancaledDate => StatusId == -1 ? ModifiedDate : null;
    }
}
