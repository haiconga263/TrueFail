using Common.Interfaces;
using Productions.UI.CultivationActivities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.CultivationActivities.Interfaces
{
    public interface ICultivationActivityQueries : IBaseQueries
    {
        Task<CultivationActivity> GetById(int id);
        Task<IEnumerable<CultivationActivity>> Gets();
    }
}
