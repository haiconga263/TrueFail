using Common.Interfaces;
using MDM.UI.Collections.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDM.UI.Collections.Interfaces
{
    public interface ICollectionInventoryQueries : IBaseQueries
    {
        Task<IEnumerable<CollectionInventory>> Gets(string condition = "");
        Task<CollectionInventory> Get(string condition = "");
        Task<CollectionInventory> GetByTraceCode(string code);
    }
}
