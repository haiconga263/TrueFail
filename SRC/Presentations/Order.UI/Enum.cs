using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI
{
    public enum RetailerOrderStatuses
    {
        Ordered = 1,
        ConfirmedOrder = 2,
        FarmerOrdered = 3,
        InConllections = 4,
        InFulfillment = 5,
        InLogistic = 6,
        Completed = 7,
        Canceled = -1
    }

    public enum FarmerOrderStatuses
    {
        BeginOrder = 1,
        ConfirmedOrder = 2,
        Completed = 3,
        Canceled = -1
    }

    public enum PlanningStatusSearchFilter
    {
        All = 0,
        UnCompleted = 1,
        Completed = 2
    }
}
