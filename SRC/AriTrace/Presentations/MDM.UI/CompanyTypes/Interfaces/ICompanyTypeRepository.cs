using Common.Interfaces;
using MDM.UI.CompanyTypes.Models;
using System.Threading.Tasks;

namespace MDM.UI.CompanyTypes.Interfaces
{
    public interface ICompanyTypeRepository: IBaseRepository
    {
        Task<int> AddAsync(CompanyType companyType);
        Task<int> UpdateAsync(CompanyType companyType);
        Task<int> DeleteAsync(int id);
    }
}
