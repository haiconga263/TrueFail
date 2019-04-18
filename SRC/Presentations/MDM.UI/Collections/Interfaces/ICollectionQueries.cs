using Common.Interfaces;
using MDM.UI.Collections.Models;
using MDM.UI.Collections.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDM.UI.Collections.Interfaces
{
    public interface ICollectionQueries : IBaseQueries
    {
        Task<string> GenarateCode();
        Task<IEnumerable<CollectionViewModel>> Gets(string condition = "");
        Task<CollectionViewModel> Get(int id);
        Task<CollectionViewModel> GetByCode(string code);
        Task<IEnumerable<CollectionViewModel>> GetsBySupervisor(int managerId);
        Task<IEnumerable<CollectionViewModel>> GetsByEmployeeId(int employeeId);
    }
}
