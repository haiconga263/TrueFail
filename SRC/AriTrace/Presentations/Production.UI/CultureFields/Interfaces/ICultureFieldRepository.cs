using Common.Interfaces;
using Production.UI.CultureFields.Models;
using System.Threading.Tasks;

namespace Production.UI.CultureFields.Interfaces
{
    public interface ICultureFieldRepository: IBaseRepository
    {
        Task<int> AddAsync(CultureField cultureField);
        Task<int> UpdateAsync(CultureField cultureField);
        Task<int> DeleteAsync(int id);
    }
}
