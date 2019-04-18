using Common.Models;
using DAL;
using MDM.UI.ScheduleActions.Enumerations;
using MDM.UI.ScheduleActions.Interfaces;
using MDM.UI.ScheduleActions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;

namespace MDM.ScheduleActions.Queries
{
    public class ScheduleActionQueries : BaseQueries, IScheduleActionQueries
    {
        public async Task<IEnumerable<ScheduleAction>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `schedule_Action`";
            return await DALHelper.Query<ScheduleAction>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<ScheduleAction> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<ScheduleAction>($"SELECT * FROM `schedule_Action` WHERE `id` = '{id}'", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<ScheduleAction>> GetActionsNeedRunAsync()
        {
            string cmd = $"SELECT * FROM `schedule_Action`WHERE `action_result` < 1 AND `down_count` > 0 ";
            return await DALHelper.Query<ScheduleAction>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
