using Common.Interfaces;
using Productions.UI.Pesticides.Models;
using System.Threading.Tasks;

namespace Productions.UI.Pesticides.Interfaces
{
    public interface IPesticideRepository : IBaseRepository
    {
        Task<int> Add(Pesticide pesticide);
        Task<int> Update(Pesticide pesticide);
        Task<int> Delete(int id);
    }
}
