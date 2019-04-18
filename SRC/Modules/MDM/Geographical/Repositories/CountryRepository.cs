using Common.Models;
using DAL;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Geographical.Repositories
{
    public class CountryRepository : BaseRepository, ICountryRepository
    {
        public async Task<int> Add(Country country)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(country);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Country country)
        {
            var cmd = $@"UPDATE `country`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {country.ModifiedBy},
                         `modified_date` = '{country.ModifiedDate.Value.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {country.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Country country)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(country);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
