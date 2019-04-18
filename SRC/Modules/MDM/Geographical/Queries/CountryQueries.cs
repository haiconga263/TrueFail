using Common.Models;
using DAL;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Geographical.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Geographical.Queries
{
    public class CountryQueries : BaseQueries, ICountryQueries
    {
        public async Task<Country> Get(int countryId)
        {
            return (await DALHelper.Query<Country>($"SELECT * FROM `country` WHERE `id` = {countryId} AND is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<CountryCommon>> GetCommons()
        {
            string cmd = "SELECT * FROM `country` WHERE is_used = 1 AND is_deleted = 0";
            return await DALHelper.Query<CountryCommon>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<Country>> Gets(string condition = "")
        {
            string cmd = "SELECT * FROM `country` WHERE is_deleted = 0";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            return await DALHelper.Query<Country>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
