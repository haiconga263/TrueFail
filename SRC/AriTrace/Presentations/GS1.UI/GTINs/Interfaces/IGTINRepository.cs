using Common.Interfaces;
using GS1.UI.GTINs.Models;
using System.Threading.Tasks;

namespace GS1.UI.GTINs.Interfaces
{
    public interface IGTINRepository: IBaseRepository
    {
        Task<int> AddAsync(GTIN gtin);
        Task<int> UpdateAsync(GTIN gtin);
        Task<int> DeleteAsync(int id);
    }
}
