using Common.Interfaces;
using Productions.UI.Methods.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.Methods.Interfaces
{
    public interface IMethodQueries : IBaseQueries
    {
        Task<Method> GetById(int id);
        Task<IEnumerable<Method>> Gets();
        Task<IEnumerable<Method>> GetAll();
    }
}
