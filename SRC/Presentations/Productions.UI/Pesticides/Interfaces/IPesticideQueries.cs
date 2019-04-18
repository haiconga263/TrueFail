using Common.Interfaces;
using Productions.UI.Pesticides.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.Pesticides.Interfaces
{
    public interface IPesticideQueries : IBaseQueries
    {
        Task<Pesticide> GetById(int id);
        Task<IEnumerable<Pesticide>> Gets();
        Task<IEnumerable<Pesticide>> GetAll();
    }
}
