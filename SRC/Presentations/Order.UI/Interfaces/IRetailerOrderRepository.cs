using Common.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IRetailerOrderRepository : IBaseRepository
    {
        Task<int> Add(RetailerOrder order);
        Task<int> Update(RetailerOrder order);

        Task<int> AddItem(RetailerOrderItem item);
        Task<int> UpdateItem(RetailerOrderItem item);
        Task<int> DeleteItems(long orderId);

        Task<int> AddAudit(RetailerOrderAudit audit);
    }
}
