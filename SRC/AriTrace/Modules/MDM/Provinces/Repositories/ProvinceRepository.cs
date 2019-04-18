using Common.Models;
using DAL;
using MDM.UI.Provinces.Interfaces;
using MDM.UI.Provinces.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Provinces.Repositories
{
    public class ProvinceRepository : BaseRepository, IProvinceRepository
    {
        public async Task<int> AddAsync(Province province)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(province);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `province` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Province province)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(province);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
