using Common.Models;
using DAL;
using Productions.UI.Cultivations.Interfaces;
using Productions.UI.Cultivations.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Cultivations.Repositories
{
    public class CultivationRepository : BaseRepository, ICultivationRepository
    {
        public async Task<int> Add(Cultivation cultivation)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(cultivation);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{Cultivation.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(Cultivation cultivation)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(cultivation);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}