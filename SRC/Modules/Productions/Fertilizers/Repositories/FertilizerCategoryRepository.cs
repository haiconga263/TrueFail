using Common.Models;
using DAL;
using Productions.UI.Fertilizers.Interfaces;
using Productions.UI.Fertilizers.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Fertilizers.Repositories
{
    public class FertilizerCategoryRepository : BaseRepository, IFertilizerCategoryRepository
    {
        public async Task<int> Add(FertilizerCategory fertilizerCategory)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(fertilizerCategory);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }


        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{FertilizerCategory.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(FertilizerCategory fertilizerCategory)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(fertilizerCategory);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}