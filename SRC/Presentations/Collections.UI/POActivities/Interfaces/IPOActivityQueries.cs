using Collections.UI.POActivities.ViewModels;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collections.UI.POActivities.Interfaces
{
    public interface IPOActivityQueries : IBaseQueries
    {
        Task<POActivityInformation> GetByIdAsync(int id);
        Task<IEnumerable<POActivityInformation>> GetByPOIdAsync(int poId);
        Task<IEnumerable<POActivityInformation>> GetsAsync(DateTime lastRequest);
    }
}
