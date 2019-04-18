using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ModelEvent
    {
        public string BeforeEvent { set; get; }
        public string AfterEvent { set; get; }
        public string BeforeRowEvent { set; get; }
        public string AfterRowEvent { set; get; }
    }
}
