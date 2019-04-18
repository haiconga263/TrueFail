using Common.Models;
using DAL;
using Productions.UI.Fertilizers.Interfaces;
using Productions.UI.Fertilizers.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Fertilizers.Repositories
{
    public class FertilizerRepository : BaseRepository, IFertilizerRepository
    {
        public async Task<int> Add(Fertilizer fertilizer)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(fertilizer);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{Fertilizer.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(Fertilizer fertilizer)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(fertilizer);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}