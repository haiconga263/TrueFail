using Common.Models;
using DAL;
using Productions.UI.Seeds.Interfaces;
using Productions.UI.Seeds.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Seeds.Queries
{
    public class SeedQueries : BaseQueries, ISeedQueries
    {
        public async Task<IEnumerable<Seed>> GetAll()
        {
            string cmd = $@"SELECT * FROM `{Seed.TABLENAME}` f WHERE f.`is_deleted` = '0'";
            return await DALHelper.Query<Seed>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<Seed>> Gets()
        {
            string cmd = $@"SELECT * FROM `{Seed.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`is_used` = '1'";
            return await DALHelper.Query<Seed>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Seed> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{Seed.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`id` = '{id}'";
            return (await DALHelper.Query<Seed>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }
    }
}
