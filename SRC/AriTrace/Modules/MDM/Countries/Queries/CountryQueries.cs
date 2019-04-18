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

namespace MDM.Countries.Queries
{
    public class CountryQueries : BaseQueries, ICountryQueries
    {
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `country` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Country>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Country>($"SELECT * FROM `country` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Country>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `country` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Country>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
