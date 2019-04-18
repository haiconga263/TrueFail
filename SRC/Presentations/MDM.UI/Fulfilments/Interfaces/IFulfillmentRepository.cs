using Common.Interfaces;
using MDM.UI.Collections.Models;
using MDM.UI.Fulfillments.Models;
using System.Threading.Tasks;

namespace MDM.UI.Fulfillments.Interfaces
{
    public interface IFulfillmentRepository : IBaseRepository
    {
        Task<int> Add(Fulfillment fulfillment);
        Task<int> Update(Fulfillment fulfillment);
        Task<int> Delete(Fulfillment fulfillment);
    }
}
