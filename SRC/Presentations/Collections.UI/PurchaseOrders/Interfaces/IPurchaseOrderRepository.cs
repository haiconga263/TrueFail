using Common.Interfaces;
using Collections.UI.PurchaseOrders.Models;
using System.Threading.Tasks;

namespace Collections.UI.PurchaseOrders.Interfaces
{
    public interface IPurchaseOrderRepository: IBaseRepository
    {
        Task<int> AddAsync(PurchaseOrder po);
        Task<bool> UpdateAsync(PurchaseOrder po);
        Task<bool> DeleteAsync(int id, int modifiedBy);

        Task<int> AddItemAsync(PurchaseOrderItem poItem);
        Task<bool> UpdateItemAsync(PurchaseOrderItem poItem);
    }
}
