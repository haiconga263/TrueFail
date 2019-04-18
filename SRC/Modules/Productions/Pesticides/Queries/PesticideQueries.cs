using Common.Models;
using DAL;
using Productions.UI.Pesticides.Interfaces;
using Productions.UI.Pesticides.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Pesticides.Queries
{
    public class PesticideQueries : BaseQueries, IPesticideQueries
    {
        public async Task<IEnumerable<Pesticide>> GetAll()
        {
            string cmd = $@"SELECT * FROM `{Pesticide.TABLENAME}` f WHERE f.`is_deleted` = '0'";
            return await DALHelper.Query<Pesticide>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Pesticide> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{Pesticide.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`id` = '{id}'";
            return (await DALHelper.Query<Pesticide>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<Pesticide>> Gets()
        {
            string cmd = $@"SELECT * FROM `{Pesticide.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`is_used` = '1'";
            return await DALHelper.Query<Pesticide>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
