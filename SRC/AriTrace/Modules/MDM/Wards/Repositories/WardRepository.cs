using Common.Models;
using DAL;
using MDM.UI.Wards.Interfaces;
using MDM.UI.Wards.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Wards.Repositories
{
    public class WardRepository : BaseRepository, IWardRepository
    {
        public async Task<int> AddAsync(Ward ward)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(ward);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `ward` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Ward ward)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(ward);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
