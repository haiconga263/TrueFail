using Common.Interfaces;
using Productions.UI.Pesticides.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.Pesticides.Interfaces
{
    public interface IPesticideCategoryQueries : IBaseQueries
    {
        Task<PesticideCategory> GetById(int id);
        Task<IEnumerable<PesticideCategory>> Gets();
        Task<IEnumerable<PesticideCategory>> GetAll();
    }
}
