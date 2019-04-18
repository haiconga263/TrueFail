using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class EventResultModel
    {
        public bool Result { set; get; } = true;
        public string ErrorMessage { set; get; } = string.Empty;
        public bool IsBubble { set; get; } = true;
    }
}
