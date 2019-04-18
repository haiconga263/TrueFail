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

namespace MDM.Districts.Queries
{
    public class DistrictQueries : BaseQueries, IDistrictQueries
    {
        public async Task<IEnumerable<District>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `district` WHERE `is_deleted` = 0";
            return await DALHelper.Query<District>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<District> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<District>($"SELECT * FROM `district` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<District>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `district` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<District>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
