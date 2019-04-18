using MDM.UI.ScheduleActions.Interfaces;
using MDM.UI.ScheduleActions.Models;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MDM.ScheduleActions.Factories;
using MDM.UI.ScheduleActions.Enumerations;

namespace MDM.ScheduleActions.Implementors
{
    public class ScheduleActionService : IScheduleActionService
    {
        private static bool _isRunning = false;

        private readonly ISettingQueries settingQueries;
        private readonly IScheduleActionQueries scheduleActionQueries;
        private readonly IScheduleActionRepository scheduleActionRepository;
        public ScheduleActionService(ISettingQueries settingQueries,
                                IScheduleActionRepository scheduleActionRepository,
                                IScheduleActionQueries scheduleActionQueries)
        {
            this.settingQueries = settingQueries;
            this.scheduleActionRepository = scheduleActionRepository;
            this.scheduleActionQueries = scheduleActionQueries;

            (this.Run()).Start();
        }

        public void Init()
        {
        }

        public async Task Run()
        {
            if (_isRunning) return;
            _isRunning = true;
            while (true)
            {
                LogHelper.GetLogger().Info($"ScheduleAction {DateTime.Now}");
                var actions = (await scheduleActionQueries.GetActionsNeedRunAsync()).ToList();
                for (int i = 0; i < actions.Count; i++)
                {
                    actions[i].DownCount--;
                    try
                    {
                        var implementor = actions[i]?.GetImplementor();
                        if (implementor == null)
                            throw new Exception("Schedule Action is invalid!");

                        await implementor.Run();
                        actions[i].ActionResult = ActionResults.success;
                    }
                    catch (Exception ex)
                    {
                        actions[i].ActionResult = ActionResults.faild;
                        actions[i].message = ex.Message ?? ex.InnerException?.Message;
                    }

                    await scheduleActionRepository.UpdateAsync(actions[i]);
                }

                int delayTime = (await settingQueries.GetValueAsync(SettingKeys.Schedule_DelayRepeat)).ToInt();
                if (delayTime == 0) break;

                await Task.Delay(delayTime);
            }
            _isRunning = false;
        }

    }
}
