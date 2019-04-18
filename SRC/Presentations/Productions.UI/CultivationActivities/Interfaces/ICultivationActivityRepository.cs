using Common.Interfaces;
using Productions.UI.CultivationActivities.Models;
using System.Threading.Tasks;

namespace Productions.UI.CultivationActivities.Interfaces
{
    public interface ICultivationActivityRepository : IBaseRepository
    {
        Task<int> Add(CultivationActivity cultivationActivity);
        Task<int> Update(CultivationActivity cultivationActivity);
        Task<int> Delete(int id);
    }
}
