using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum CachingType
    {
        SqlCaching,
        GlobalMemoryCaching,
        SessionCaching
    }

    public enum JobType
    {
        Interval,
        Timming,
        OnlyOne
    }

    public enum UserHttpCode
    {
        Error = -1,
        Success = 0,
        Warning = 1
    }

    public enum IntergrationHandleType
    {
        Insert,
        Update,
        Delete
    }
	public enum Status
	{
		New,
		Read,
		Send
	}
}
