using Common.Interfaces;
using Productions.UI.Fertilizers.Models;
using System.Threading.Tasks;

namespace Productions.UI.Fertilizers.Interfaces
{
    public interface IFertilizerCategoryRepository : IBaseRepository
    {
        Task<int> Add(FertilizerCategory fertilizerCategory);
        Task<int> Update(FertilizerCategory fertilizerCategory);
        Task<int> Delete(int id);
    }
}
