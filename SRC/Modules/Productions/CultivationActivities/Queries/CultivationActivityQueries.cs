using Common.Models;
using DAL;
using Productions.UI.CultivationActivities.Interfaces;
using Productions.UI.CultivationActivities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.CultivationActivities.Queries
{
    public class CultivationActivityQueries : BaseQueries, ICultivationActivityQueries
    {
        public async Task<IEnumerable<CultivationActivity>> Gets()
        {
            string cmd = $@"SELECT * FROM `{CultivationActivity.TABLENAME}` f WHERE f.`is_deleted` = '0'";
            return await DALHelper.Query<CultivationActivity>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<CultivationActivity> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{CultivationActivity.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`id` = '{id}'";
            return (await DALHelper.Query<CultivationActivity>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }
    }
}
