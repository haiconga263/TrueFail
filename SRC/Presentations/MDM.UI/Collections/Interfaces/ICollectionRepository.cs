using Common.Interfaces;
using MDM.UI.Collections.Models;
using System.Threading.Tasks;

namespace MDM.UI.Collections.Interfaces
{
    public interface ICollectionRepository : IBaseRepository
    {
        Task<int> Add(Collection collection);
        Task<int> Update(Collection collection);
        Task<int> Delete(Collection collection);
    }
}
