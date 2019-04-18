using Common.Models;
using DAL;
using Productions.UI.Seeds.Interfaces;
using Productions.UI.Seeds.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Seeds.Repositories
{
    public class SeedRepository : BaseRepository, ISeedRepository
    {
        public async Task<int> Add(Seed seed)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(seed);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{Seed.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(Seed seed)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(seed);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}