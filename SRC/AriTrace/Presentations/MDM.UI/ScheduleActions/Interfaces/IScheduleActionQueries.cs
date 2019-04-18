using Common.Interfaces;
using MDM.UI.ScheduleActions.Enumerations;
using MDM.UI.ScheduleActions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.ScheduleActions.Interfaces
{
    public interface IScheduleActionQueries : IBaseQueries
    {
        Task<ScheduleAction> GetByIdAsync(int id);
        Task<IEnumerable<ScheduleAction>> GetActionsNeedRunAsync();
        Task<IEnumerable<ScheduleAction>> GetAllAsync();
    }
}
