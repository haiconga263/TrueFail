using Common.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IFarmerRetailerOrderItemQueries : IBaseQueries
    {
        Task<FarmerRetailerOrderItems> Get(long id);
        Task<IEnumerable<FarmerRetailerOrderItems>> GetByBC(long bc);
    }
}
