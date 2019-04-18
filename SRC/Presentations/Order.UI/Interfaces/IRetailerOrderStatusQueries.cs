using Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IRetailerOrderStatusQueries : IBaseQueries
    {
        Task<IEnumerable<Order.UI.Models.RetailerOrderStatus>> Gets();
    }
}
