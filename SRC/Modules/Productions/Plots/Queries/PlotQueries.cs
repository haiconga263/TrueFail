using Common.Models;
using DAL;
using Productions.UI.Plots.Interfaces;
using Productions.UI.Plots.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productions.Plots.Queries
{
    public class PlotQueries : BaseQueries, IPlotQueries
    {
        public async Task<IEnumerable<Plot>> Gets()
        {
            string cmd = $@"SELECT * FROM `{Plot.TABLENAME}` f WHERE f.`is_deleted` = '0'";
            return await DALHelper.Query<Plot>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Plot> GetById(int id)
        {
            string cmd = $@"SELECT * FROM `{Plot.TABLENAME}` f WHERE f.`is_deleted` = '0' AND f.`id` = '{id}'";
            return (await DALHelper.Query<Plot>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }
    }
}
