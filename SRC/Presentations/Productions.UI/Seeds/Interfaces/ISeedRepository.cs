using Common.Interfaces;
using Productions.UI.Seeds.Models;
using System.Threading.Tasks;

namespace Productions.UI.Seeds.Interfaces
{
    public interface ISeedRepository : IBaseRepository
    {
        Task<int> Add(Seed seed);
        Task<int> Update(Seed seed);
        Task<int> Delete(int id);
    }
}
