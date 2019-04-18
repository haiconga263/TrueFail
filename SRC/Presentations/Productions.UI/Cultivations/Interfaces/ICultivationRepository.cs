using Common.Interfaces;
using Productions.UI.Cultivations.Models;
using System.Threading.Tasks;

namespace Productions.UI.Cultivations.Interfaces
{
    public interface ICultivationRepository : IBaseRepository
    {
        Task<int> Add(Cultivation cultivation);
        Task<int> Update(Cultivation cultivation);
        Task<int> Delete(int id);
    }
}
