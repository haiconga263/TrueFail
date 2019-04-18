using Common.Interfaces;
using MDM.UI.Companies.Models;
using MDM.UI.Companies.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Companies.Interfaces
{
    public interface ICompanyQueries : IBaseQueries
    {
        Task<CompanyViewModel> GetByIdAsync(int id);

        Task<IEnumerable<CompanyViewModel>> GetsAsync();
        Task<IEnumerable<CompanyViewModel>> GetAllAsync();
        Task<CompanyViewModel> GetByUserIdAsync(int id);
    }
}
