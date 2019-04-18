using Common.Models;
using DAL;
using Productions.UI.Cultivations.Interfaces;
using Productions.UI.Cultivations.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Cultivations.Queries
{
    public class CultivationQueries : BaseQueries, ICultivationQueries
    {
        public async Task<IEnumerable<Cultivation>> Gets()
        {
            string cmd = $@"SELECT * FROM `{Cultivation.TABLENAME}` f WHERE f.`is_deleted` = '0'";
            return await DALHelper.Query<Cultivation>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Cultivation> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{Cultivation.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`id` = '{id}'";
            return (await DALHelper.Query<Cultivation>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }
    }
}
