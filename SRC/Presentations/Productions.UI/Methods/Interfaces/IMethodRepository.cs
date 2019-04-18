using Common.Interfaces;
using Productions.UI.Methods.Models;
using System.Threading.Tasks;

namespace Productions.UI.Methods.Interfaces
{
    public interface IMethodRepository : IBaseRepository
    {
        Task<int> Add(Method method);
        Task<int> Update(Method method);
        Task<int> Delete(int id);
    }
}
