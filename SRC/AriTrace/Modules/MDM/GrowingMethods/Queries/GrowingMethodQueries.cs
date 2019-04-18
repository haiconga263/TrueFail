using Common.Models;
using DAL;
using MDM.UI.GrowingMethods.Interfaces;
using MDM.UI.GrowingMethods.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.GrowingMethods.Queries
{
    public class GrowingMethodQueries : BaseQueries, IGrowingMethodQueries
    {
        public async Task<IEnumerable<GrowingMethod>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `growing_method` WHERE `is_deleted` = 0";
            return await DALHelper.Query<GrowingMethod>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<GrowingMethod> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<GrowingMethod>($"SELECT * FROM `growing_method` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<GrowingMethod>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `growing_method` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<GrowingMethod>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
