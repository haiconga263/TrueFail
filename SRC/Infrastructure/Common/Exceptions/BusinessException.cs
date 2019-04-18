using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class BusinessException : Exception
    {
        public object DataTranfer { get; set; }
        public BusinessException(string message = "") : base(message) { }
        public BusinessException(string message, object data) : base(message) { DataTranfer = data; }
    }
}
