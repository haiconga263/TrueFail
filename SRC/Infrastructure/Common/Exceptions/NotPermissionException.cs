using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class NotPermissionException : Exception
    {
        public NotPermissionException() : base("No permission to this action")
        {

        }
    }
}
