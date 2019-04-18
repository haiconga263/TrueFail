using System;
using System.Collections.Generic;
using System.Text;

namespace Abivin.Integration.UI
{
    public enum OrderStatuses
    {
        Open,
        InLogistic,
        CompletedFull,
        CompletedHalf,
        Failed
    }
}
