using MDM.UI.Accounts.Models;
using Production.UI.CultureFields.Models;
using Production.UI.MaterialHistories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Production.UI.MaterialHistories.ViewModels
{
    public class MaterialHistoryInfomation : MaterialHistory
    {
        public CultureField CultureField { get; set; }
        public Account UserCreated { get; set; }
        public Account UserDeleted { get; set; }
    }
}
