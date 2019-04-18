using MDM.UI.ScheduleActions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.ScheduleActions.Interfaces
{
    public interface IActionImplementor
    {
        Task Run();
    }
}
