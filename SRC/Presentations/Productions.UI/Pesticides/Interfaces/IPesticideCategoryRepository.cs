using Common.Interfaces;
using Productions.UI.Pesticides.Models;
using System.Threading.Tasks;

namespace Productions.UI.Pesticides.Interfaces
{
    public interface IPesticideCategoryRepository : IBaseRepository
    {
        Task<int> Add(PesticideCategory pesticideCategory);
        Task<int> Update(PesticideCategory pesticideCategory);
        Task<int> Delete(int id);
    }
}
