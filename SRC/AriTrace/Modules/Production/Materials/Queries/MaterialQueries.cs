using Common.Models;
using DAL;
using Production.UI.Materials.Interfaces;
using Production.UI.Materials.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Materials.Queries
{
    public class MaterialQueries : BaseQueries, IMaterialQueries
    {
        public async Task<IEnumerable<Material>> GetAllAsync(int? partnerId = null)
        {
            string cmd = $"SELECT * FROM `material` WHERE `is_deleted` = 0";
            if ((partnerId ?? 0) > 0)
                cmd += $" AND `partner_id`='{partnerId}'";

            return await DALHelper.Query<Material>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Material> GetByIdAsync(int id)
        {
            string cmd = $"SELECT * FROM `material` WHERE `is_deleted` = 0 AND `id` = {id}";
            return (await DALHelper.Query<Material>(cmd, dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Material>> GetsAsync(int? partnerId = null)
        {
            string cmd = $"SELECT * FROM `material` WHERE `is_used` = 1 AND `is_deleted` = 0";
            if ((partnerId ?? 0) > 0)
                cmd += $" AND `partner_id`='{partnerId}'";

            return await DALHelper.Query<Material>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
