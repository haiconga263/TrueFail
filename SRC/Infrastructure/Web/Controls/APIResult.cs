using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Controls
{
    public class APIResult
    {
        public object Data { get; set; }
        public int Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
