using Common.Interfaces;
using Productions.UI.Cultivations.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.Cultivations.Interfaces
{
    public interface ICultivationQueries : IBaseQueries
    {
        Task<Cultivation> GetById(int id);
        Task<IEnumerable<Cultivation>> Gets();
    }
}
