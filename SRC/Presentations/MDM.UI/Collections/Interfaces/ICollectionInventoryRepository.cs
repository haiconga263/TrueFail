using Common.Interfaces;
using MDM.UI.Collections.Models;
using System.Threading.Tasks;

namespace MDM.UI.Collections.Interfaces
{
    public interface ICollectionInventoryRepository : IBaseRepository
    {
        Task<int> Add(CollectionInventory inventory);
        Task<int> Update(CollectionInventory inventory);
        Task<int> Delete(long inventoryId);
    }
}
