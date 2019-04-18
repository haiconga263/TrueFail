using Common.Interfaces;
using MDM.UI.Distributions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Distributions.Interfaces
{
    public interface IDistributionEmployeeQueries : IBaseQueries
    {
        Task<IEnumerable<DistributionEmployee>> Gets(string condition = "");
        Task<IEnumerable<DistributionEmployee>> GetsByDistribution(int distributionId);
    }
}
