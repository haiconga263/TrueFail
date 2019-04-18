using Common.Interfaces;
using Fulfillments.UI.Models;
using Fulfillments.UI.ViewModels;
using Order.UI.Models;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fulfillments.UI.Interfaces
{
    public interface IFulfillmentFROrderQueries : IBaseQueries
    {
        Task<IEnumerable<FulfillmentRetailerOrderViewModel>> GetRetailerOrderForPackByFulId(string fulfillmentId);
        Task<IEnumerable<FulfillmentRetailerOrderViewModel>> GetRetailerOrderForPack(string retailerOrderId);
        Task<IEnumerable<FulfillmentTeam>> GetTeam();
    }
}
