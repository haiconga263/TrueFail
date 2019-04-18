using System;
using System.Collections.Generic;
using System.Text;

namespace QC.UI.Model
{
    public class QCCheckList
    {
        public int ID { get; set; }
        public Dictionary<string, bool> CheckList { get; set; }
    }
}
