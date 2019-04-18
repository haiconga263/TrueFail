using MDM.ScheduleActions.Implementors;
using MDM.UI.ScheduleActions.Enumerations;
using MDM.UI.ScheduleActions.Interfaces;
using MDM.UI.ScheduleActions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.ScheduleActions.Factories
{
    public static class ScheduleActionFactory
    {
        public static IActionImplementor GetImplementor(this ScheduleAction scheduleAction)
        {
            if (scheduleAction == null) return null;
            switch (scheduleAction.ActionType)
            {
                case ActionTypes.deletefile:
                    return new RemoveFileImplementor(scheduleAction);
                default:
                    return null;
            }
        }

        public static ScheduleAction GetScheduleAction(this RemoveFileAction removeFileAction, int userId)
        {
            return new ScheduleAction()
            {
                ActionResult = ActionResults.notstart,
                ActionType = ActionTypes.deletefile,
                Data = JsonConvert.SerializeObject(removeFileAction),
                DownCount = 3,
                CreatedDate = DateTime.Now,
                CreatedBy = userId
            };
        }
    }
}
