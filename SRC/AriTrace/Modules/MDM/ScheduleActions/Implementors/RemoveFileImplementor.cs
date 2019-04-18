using MDM.ScheduleActions.Commons;
using MDM.UI.ScheduleActions.Interfaces;
using MDM.UI.ScheduleActions.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MDM.ScheduleActions.Implementors
{
    public class RemoveFileImplementor : BaseActionImplementor<RemoveFileAction>, IActionImplementor
    {
        public RemoveFileImplementor(ScheduleAction scheduleAction) : base(scheduleAction)
        {
        }

        public override Task Run()
        {
            if (File.Exists(Action.FilePath))
            {
                File.Delete(Action.FilePath);
            }
            return null;
        }
    }
}
