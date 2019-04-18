using Common.Interfaces;
using MDM.UI.Collections.Models;
using MDM.UI.Distributions.Models;
using System.Threading.Tasks;

namespace MDM.UI.Distributions.Interfaces
{
    public interface IDistributionRepository : IBaseRepository
    {
        Task<int> Add(Distribution distribution);
        Task<int> Update(Distribution distribution);
        Task<int> Delete(Distribution distribution);
    }
}
