using Common.Models;
using DAL;
using Productions.UI.Fertilizers.Interfaces;
using Productions.UI.Fertilizers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Fertilizers.Queries
{
    public class FertilizerCategoryQueries : BaseQueries, IFertilizerCategoryQueries
    {
        public async Task<IEnumerable<FertilizerCategory>> GetAll()
        {
            string cmd = $@"SELECT * FROM `{FertilizerCategory.TABLENAME}` fc WHERE fc.`is_deleted` = '0'";
            return await DALHelper.Query<FertilizerCategory>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<FertilizerCategory> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{FertilizerCategory.TABLENAME}` fc WHERE fc.`is_deleted` = '0' AND fc.`id` = '{id}'";
            return (await DALHelper.Query<FertilizerCategory>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<FertilizerCategory>> Gets()
        {
            string cmd = $@"SELECT * FROM `{FertilizerCategory.TABLENAME}` fc WHERE fc.`is_deleted` = '0' AND fc.`is_used` = '1'";
            return await DALHelper.Query<FertilizerCategory>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
