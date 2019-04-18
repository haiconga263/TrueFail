using Common.Interfaces;
using MDM.UI.Companies.Models;
using System.Threading.Tasks;

namespace MDM.UI.Companies.Interfaces
{
    public interface ICompanyRepository: IBaseRepository
    {
        Task<int> AddAsync(Company company);
        Task<int> UpdateAsync(Company company);
        Task<int> DeleteAsync(int id);
    }
}
