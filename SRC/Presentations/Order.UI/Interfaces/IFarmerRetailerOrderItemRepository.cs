using Common.Interfaces;
using Order.UI.Models;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IFarmerRetailerOrderItemRepository : IBaseRepository
    {
        Task<int> Add(FarmerRetailerOrderItems item);
        Task<int> Update(FarmerRetailerOrderItems item);
        Task<int> Delete(long itemId);
        Task<int> Delete(string condition);
    }
}
