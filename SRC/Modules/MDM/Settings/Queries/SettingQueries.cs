using Common.Models;
using DAL;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using MDM.UI.Settings.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;

namespace MDM.Settings.Queries
{
    public class SettingQueries : BaseQueries, ISettingQueries
    {
        private readonly ISettingRepository _settingRepository = null;
        public SettingQueries(ISettingRepository settingRepository)
        {
            this._settingRepository = settingRepository;
        }
        public async Task<IEnumerable<Setting>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `setting`";
            return await DALHelper.Query<Setting>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Setting>($"SELECT * FROM `setting` WHERE `id` = '{id}'", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<Setting> GetByKeyAsync(string key)
        {
            var rs = (await DALHelper.Query<Setting>($"SELECT * FROM `setting` WHERE `name` = '{key}'", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
            if (rs == null)
            {
                rs = new Setting()
                {
                    Name = key,
                    Value = "",
                    Description = "",
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = 0,
                    ModifiedDate = DateTime.Now,
                };
                rs.Id = await _settingRepository.AddAsync(rs);
            }
            return rs;
        }

        public async Task<IEnumerable<Setting>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `setting`";
            return await DALHelper.Query<Setting>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<Setting>> GetsByPrefixAsync(string prefix)
        {
            string cmd = $"SELECT * FROM `setting` WHERE `name` LIKE '{prefix}.%'";
            return await DALHelper.Query<Setting>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<string> GetValueAsync(string key)
        {
            return (await GetByKeyAsync(key))?.Value ?? "";
        }
    }
}
