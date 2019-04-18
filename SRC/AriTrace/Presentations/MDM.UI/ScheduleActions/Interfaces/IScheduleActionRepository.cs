using Common.Interfaces;
using MDM.UI.ScheduleActions.Models;
using System.Threading.Tasks;

namespace MDM.UI.ScheduleActions.Interfaces
{
    public interface IScheduleActionRepository : IBaseRepository
    {
        Task<int> AddAsync(ScheduleAction scheduleAction);
        Task<int> UpdateAsync(ScheduleAction scheduleAction);
        Task<int> DeleteAsync(int id);
    }
}
