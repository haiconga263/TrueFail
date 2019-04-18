using Common.Models;
using DAL;
using Productions.UI.Methods.Interfaces;
using Productions.UI.Methods.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Methods.Queries
{
    public class MethodQueries : BaseQueries, IMethodQueries
    {
        public async Task<IEnumerable<Method>> GetAll()
        {
            string cmd = $@"SELECT * FROM `{Method.TABLENAME}` f WHERE f.`is_deleted` = '0'";
            return await DALHelper.Query<Method>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<Method>> Gets()
        {
            string cmd = $@"SELECT * FROM `{Method.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`is_used` = '1'";
            return await DALHelper.Query<Method>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Method> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{Method.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`id` = '{id}'";
            return (await DALHelper.Query<Method>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }
    }
}
