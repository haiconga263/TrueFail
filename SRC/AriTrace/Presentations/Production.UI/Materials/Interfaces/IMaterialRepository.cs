using Common.Interfaces;
using Production.UI.Materials.Models;
using System.Threading.Tasks;

namespace Production.UI.Materials.Interfaces
{
    public interface IMaterialRepository: IBaseRepository
    {
        Task<int> AddAsync(Material material);
        Task<int> UpdateAsync(Material material);
        Task<int> DeleteAsync(int id);
    }
}
