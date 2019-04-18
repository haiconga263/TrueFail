using Common.Models;
using DAL;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Distributions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Distributions.Queries
{
    public class DistributionEmployeeQueries : BaseQueries, IDistributionEmployeeQueries
    {
        public async Task<IEnumerable<DistributionEmployee>> Gets(string condition = "")
        {
            return await DALHelper.ExecuteQuery<DistributionEmployee>($"SELECT * FROM `distribution_employee` {(string.IsNullOrEmpty(condition) ? string.Empty : $"WHERE {condition}")}", dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<DistributionEmployee>> GetsByDistribution(int distributionId)
        {
            return await DALHelper.ExecuteQuery<DistributionEmployee>($"SELECT * FROM `distribution_employee` WHERE distribution_id = {distributionId}", dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
