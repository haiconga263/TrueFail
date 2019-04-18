using MDM.UI.Employees.Models;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI.ViewModels
{
    public class RetailerOrderAuditViewModel : RetailerOrderAudit
    {
        public Employee By { set; get; }
    }
}
