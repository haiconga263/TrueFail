using Common.Interfaces;
using Productions.UI.Fertilizers.Models;
using System.Threading.Tasks;

namespace Productions.UI.Fertilizers.Interfaces
{
    public interface IFertilizerRepository : IBaseRepository
    {
        Task<int> Add(Fertilizer fertilizer);
        Task<int> Update(Fertilizer fertilizer);
        Task<int> Delete(int id);
    }
}
