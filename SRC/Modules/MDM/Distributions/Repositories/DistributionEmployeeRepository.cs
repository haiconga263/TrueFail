using Common.Models;
using DAL;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Distributions.Models;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Distributions.Repositories
{
    public class DistributionEmployeeRepository : BaseRepository, IDistributionEmployeeRepository
    {
        public async Task<int> Add(DistributionEmployee employee)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(employee);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(int distributionEmployeeId)
        {
            var cmd = $@"DELETE FROM `distribution_employee` WHERE id = {distributionEmployeeId}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(DistributionEmployee employee)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(employee);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
