using Common.Models;
using DAL;
using Productions.UI.Pesticides.Interfaces;
using Productions.UI.Pesticides.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Pesticides.Queries
{
    public class PesticideCategoryQueries : BaseQueries, IPesticideCategoryQueries
    {
        public async Task<IEnumerable<PesticideCategory>> GetAll()
        {
            string cmd = $@"SELECT * FROM `{PesticideCategory.TABLENAME}` fc WHERE fc.`is_deleted` = '0'";
            return await DALHelper.Query<PesticideCategory>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<PesticideCategory> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{PesticideCategory.TABLENAME}` fc WHERE fc.`is_deleted` = '0' AND fc.`id` = '{id}'";
            return (await DALHelper.Query<PesticideCategory>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<PesticideCategory>> Gets()
        {
            string cmd = $@"SELECT * FROM `{PesticideCategory.TABLENAME}` fc WHERE fc.`is_deleted` = '0' AND fc.`is_used` = '1'";
            return await DALHelper.Query<PesticideCategory>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
