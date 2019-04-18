using Common.Interfaces;
using Productions.UI.Seeds.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.Seeds.Interfaces
{
    public interface ISeedQueries : IBaseQueries
    {
        Task<Seed> GetById(int id);
        Task<IEnumerable<Seed>> Gets();
        Task<IEnumerable<Seed>> GetAll();
    }
}
