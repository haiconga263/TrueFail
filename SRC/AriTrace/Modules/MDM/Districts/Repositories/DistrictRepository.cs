using Common.Models;
using DAL;
using MDM.UI.Districts.Interfaces;
using MDM.UI.Districts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Districts.Repositories
{
    public class DistrictRepository : BaseRepository, IDistrictRepository
    {
        public async Task<int> AddAsync(District district)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(district);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `district` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(District district)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(district);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
