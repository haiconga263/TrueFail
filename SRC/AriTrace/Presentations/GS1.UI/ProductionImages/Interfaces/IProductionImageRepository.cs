using Common.Interfaces;
using GS1.UI.ProductionImages.Models;
using System.Threading.Tasks;

namespace GS1.UI.ProductionImages.Interfaces
{
    public interface IProductionImageRepository: IBaseRepository
    {
        Task<int> AddAsync(ProductionImage productionImage);
        Task<int> UpdateAsync(ProductionImage productionImage);
        Task<int> DeleteAsync(int id);
    }
}
