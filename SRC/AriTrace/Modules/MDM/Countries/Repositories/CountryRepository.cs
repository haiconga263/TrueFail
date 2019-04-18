using Common.Models;
using DAL;
using MDM.UI.Countries.Interfaces;
using MDM.UI.Countries.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Countries.Repositories
{
    public class CountryRepository : BaseRepository, ICountryRepository
    {
        public async Task<int> AddAsync(Country country)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(country);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `country` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Country country)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(country);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
