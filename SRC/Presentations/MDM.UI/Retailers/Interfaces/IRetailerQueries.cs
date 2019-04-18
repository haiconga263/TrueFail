using Common.Interfaces;
using MDM.UI.Retailers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Retailers.Interfaces
{
    public interface IRetailerQueries : IBaseQueries
    {
        Task<IEnumerable<RetailerViewModel>> Gets(string condition = "");
        Task<RetailerViewModel> Get(int id);
        Task<RetailerViewModel> GetByUserId(int userId);
        Task<IEnumerable<RetailerLocationViewModel>> GetRetailerLocations(int retailerId);
        Task<IEnumerable<RetailerLocationViewModel>> GetRetailerLocationsByUser(int userId);
        Task<RetailerLocationViewModel> GetRetailerLocation(int retailerLocationId);

        Task<string> GenarateLocationCode();
    }
}
