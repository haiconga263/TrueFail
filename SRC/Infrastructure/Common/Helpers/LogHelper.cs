using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Common.Helpers
{
    public class LogHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static log4net.ILog GetLogger()
        {
            StackTrace stackTrace = new StackTrace();
            log4net.ThreadContext.Properties["method"] = stackTrace.GetFrame(1).GetMethod().Name;
            return log4net.LogManager.GetLogger(stackTrace.GetFrame(1).GetMethod().DeclaringType);
        }
    }
}
