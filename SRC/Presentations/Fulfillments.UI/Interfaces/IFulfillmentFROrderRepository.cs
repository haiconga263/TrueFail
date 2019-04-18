using Common.Interfaces;
using Fulfillments.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fulfillments.UI.Interfaces
{
    public interface IFulfillmentFROrderRepository : IBaseRepository
    {
        Task<int> Add(FulfillmentFROrderViewModel fulFr);
        Task<int> AddItems(FulfillmentFROrderItemViewModel fulFrItems);
    }
}
