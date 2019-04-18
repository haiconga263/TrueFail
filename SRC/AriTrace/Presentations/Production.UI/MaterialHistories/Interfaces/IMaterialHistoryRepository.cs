using Common.Interfaces;
using Production.UI.MaterialHistories.Models;
using System.Threading.Tasks;

namespace Production.UI.MaterialHistories.Interfaces
{
    public interface IMaterialHistoryRepository: IBaseRepository
    {
        Task<int> AddAsync(MaterialHistory materialHistory);
        Task<int> UpdateAsync(MaterialHistory materialHistory);
        Task<int> DeleteAsync(int id);
    }
}
