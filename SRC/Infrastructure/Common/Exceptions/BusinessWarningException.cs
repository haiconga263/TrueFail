using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class BusinessWarningException : Exception
    {
        public object DataTranfer { get; set; }
        public BusinessWarningException(string message = "") : base(message) { }
        public BusinessWarningException(string message, object data) : base(message) { DataTranfer = data; }
    }
}
