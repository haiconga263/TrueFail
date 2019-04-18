using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.UI.Reports.ViewModels
{
    public class ReportByEmployeeViewModel
    {
        public int EmployeeId { set; get; }
        public int TotalOrder { set; get; }
        public decimal TotalAmount { set; get; }
        public int CanceledCount { set; get; }
        public int CompletedCount { set; get; }
    }
}
