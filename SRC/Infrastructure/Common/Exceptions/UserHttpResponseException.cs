using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class UserHttpResponseException : Exception
    {
        public UserHttpResponseException() : base("There's a exception when request http")
        {

        }
    }
}
