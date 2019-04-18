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

namespace MDM.Wards.Queries
{
    public class WardQueries : BaseQueries, IWardQueries
    {
        public async Task<IEnumerable<Ward>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `ward` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Ward>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Ward> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Ward>($"SELECT * FROM `ward` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Ward>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `ward` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Ward>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
