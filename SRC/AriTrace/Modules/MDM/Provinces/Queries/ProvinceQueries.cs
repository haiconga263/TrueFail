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

namespace MDM.Provinces.Queries
{
    public class ProvinceQueries : BaseQueries, IProvinceQueries
    {
        public async Task<IEnumerable<Province>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `province` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Province>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Province> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Province>($"SELECT * FROM `province` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Province>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `province` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Province>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
