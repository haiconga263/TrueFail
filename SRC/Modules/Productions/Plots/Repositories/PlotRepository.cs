using Common.Models;
using DAL;
using Productions.UI.Plots.Interfaces;
using Productions.UI.Plots.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Plots.Repositories
{
    public class PlotRepository : BaseRepository, IPlotRepository
    {
        public async Task<int> Add(Plot plot)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(plot);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int id)
        {
            var cmd = $"delete from `{Plot.TABLENAME}` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(Plot plot)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(plot);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}