using System.Linq;
using System.Threading.Tasks;
using Collections.UI.POActivities.Interfaces;
using Collections.UI.POActivities.Models;
using Collections.UI.PurchaseOrders.Interfaces;
using Collections.UI.PurchaseOrders.Models;
using Common.Models;
using DAL;

namespace Collections.POActivities.Repositories
{
    public class POActivityRepository : BaseRepository, IPOActivityRepository
    {
        public async Task<int> AddAsync(POActivity poActivity)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(poActivity);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }
    }
}
