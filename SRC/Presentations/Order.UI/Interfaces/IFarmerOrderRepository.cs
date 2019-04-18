using Common.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IFarmerOrderRepository : IBaseRepository
    {
        Task<int> Add(FarmerOrder order);
        Task<int> Update(FarmerOrder order);

        Task<int> AddItem(FarmerOrderItem item);
        Task<int> UpdateItem(FarmerOrderItem item);
        Task<int> DeleteItems(long orderId);
    }
}
