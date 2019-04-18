using Common.Interfaces;
using MDM.UI.Distributions.Models;
using System.Threading.Tasks;

namespace MDM.UI.Distributions.Interfaces
{
    public interface IDistributionEmployeeRepository : IBaseRepository
    {
        Task<int> Add(DistributionEmployee employee);
        Task<int> Update(DistributionEmployee employee);
        Task<int> Delete(int distributionEmployeeId);
    }
}
