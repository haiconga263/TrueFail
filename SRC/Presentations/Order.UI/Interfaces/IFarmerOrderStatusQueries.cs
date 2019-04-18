using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IFarmerOrderStatusQueries : IBaseQueries
    {
        Task<IEnumerable<Order.UI.Models.FarmerOrderStatus>> Gets();
    }
}
