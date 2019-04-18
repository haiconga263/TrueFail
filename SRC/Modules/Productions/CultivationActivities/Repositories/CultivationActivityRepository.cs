using Common.Models;
using DAL;
using Productions.UI.CultivationActivities.Interfaces;
using Productions.UI.CultivationActivities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.CultivationActivities.Repositories
{
    public class CultivationActivityRepository : BaseRepository, ICultivationActivityRepository
    {
        public async Task<int> Add(CultivationActivity cultivationActivity)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(cultivationActivity);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{CultivationActivity.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(CultivationActivity cultivationActivity)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(cultivationActivity);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}