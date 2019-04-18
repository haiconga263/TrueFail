using Common.Interfaces;
using GS1.UI.Productions.Models;
using System.Threading.Tasks;

namespace GS1.UI.Productions.Interfaces
{
    public interface IProductionRepository: IBaseRepository
    {
        Task<int> AddAsync(Production production);
        Task<int> UpdateAsync(Production production);
        Task<int> DeleteAsync(int id);
    }
}
