using Common.Models;
using DAL;
using MDM.UI.Settings.Interfaces;
using MDM.UI.Settings.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Settings.Repositories
{
    public class SettingRepository : BaseRepository, ISettingRepository
    {
        public async Task<int> AddAsync(Setting setting)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(setting);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `setting` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Setting setting)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(setting);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
