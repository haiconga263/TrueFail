using Common.Interfaces;
using MDM.UI.CompanyTypes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.CompanyTypes.Interfaces
{
    public interface ICompanyTypeQueries : IBaseQueries
    {
        Task<CompanyType> GetByIdAsync(int id);
        Task<IEnumerable<CompanyType>> GetsAsync();
        Task<IEnumerable<CompanyType>> GetAllAsync();
    }
}
