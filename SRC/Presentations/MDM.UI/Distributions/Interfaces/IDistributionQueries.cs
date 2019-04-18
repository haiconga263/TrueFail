using Common.Interfaces;
using MDM.UI.Collections.Models;
using MDM.UI.Collections.ViewModels;
using MDM.UI.Distributions.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDM.UI.Distributions.Interfaces
{
    public interface IDistributionQueries : IBaseQueries
    {
        Task<string> GenarateCode();
        Task<IEnumerable<DistributionViewModel>> Gets(string condition = "");
        Task<DistributionViewModel> Get(int id);
        Task<DistributionViewModel> GetByCode(string code);
        Task<IEnumerable<DistributionViewModel>> GetsBySupervisor(int managerId);
        Task<IEnumerable<DistributionViewModel>> GetsByEmployeeId(int employeeId);
    }
}
