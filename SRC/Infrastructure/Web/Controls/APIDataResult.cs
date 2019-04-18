using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Web.Controls
{
    public class APIDataResult<T>
    {
        public T Data { get; set; }
        public int Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
