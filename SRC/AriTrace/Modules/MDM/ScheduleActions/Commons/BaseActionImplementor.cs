using MDM.UI.ScheduleActions.Interfaces;
using MDM.UI.ScheduleActions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MDM.ScheduleActions.Commons
{
    public abstract class BaseActionImplementor<T> : IActionImplementor
    {
        public T Action { get; set; }

        public BaseActionImplementor(ScheduleAction scheduleAction)
        {
            this.Action = JsonConvert.DeserializeObject<T>(scheduleAction.Data);
        }

        public abstract Task Run();
    }
}
