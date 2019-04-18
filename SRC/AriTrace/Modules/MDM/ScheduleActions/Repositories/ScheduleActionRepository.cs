using Common.Models;
using DAL;
using MDM.UI.ScheduleActions.Interfaces;
using MDM.UI.ScheduleActions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDM.UI.ScheduleActions.Enumerations;
using Newtonsoft.Json;

namespace MDM.ScheduleActions.Repositories
{
    public class ScheduleActionRepository : BaseRepository, IScheduleActionRepository
    {
        public async Task<int> AddAsync(ScheduleAction scheduleAction)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(scheduleAction);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `schedule_action` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(ScheduleAction scheduleAction)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(scheduleAction);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
