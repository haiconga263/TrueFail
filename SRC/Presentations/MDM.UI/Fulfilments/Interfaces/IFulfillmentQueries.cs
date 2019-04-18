using Common.Interfaces;
using MDM.UI.Fulfillments.Models;
using MDM.UI.Fulfillments.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDM.UI.Fulfillments.Interfaces
{
    public interface IFulfillmentQueries : IBaseQueries
    {
        Task<string> GenarateCode();
        Task<IEnumerable<Fulfillment>> GetAllFufillmentAsync();
        Task<IEnumerable<FulfillmentViewModel>> Gets(string condition = "");
        Task<FulfillmentViewModel> Get(int id);
        Task<FulfillmentViewModel> GetByCode(string code);
        Task<IEnumerable<FulfillmentViewModel>> GetsBySupervisor(int managerId);
    }
}
