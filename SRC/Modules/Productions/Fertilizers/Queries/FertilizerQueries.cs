using Common.Models;
using DAL;
using Productions.UI.Fertilizers.Interfaces;
using Productions.UI.Fertilizers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Fertilizers.Queries
{
    public class FertilizerQueries : BaseQueries, IFertilizerQueries
    {
        public async Task<IEnumerable<Fertilizer>> GetAll()
        {
            string cmd = $@"SELECT * FROM `{Fertilizer.TABLENAME}` f WHERE f.`is_deleted` = '0'";
            return await DALHelper.Query<Fertilizer>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Fertilizer> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{Fertilizer.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`id` = '{id}'";
            return (await DALHelper.Query<Fertilizer>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<Fertilizer>> Gets()
        {
            string cmd = $@"SELECT * FROM `{Fertilizer.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`is_used` = '1'";
            return await DALHelper.Query<Fertilizer>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
