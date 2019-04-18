using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.ScheduleActions.Interfaces
{
    public interface IScheduleActionService
    {
        void Init();
        Task Run();
    }
}
