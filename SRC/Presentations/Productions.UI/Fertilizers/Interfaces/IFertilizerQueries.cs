using Common.Interfaces;
using Productions.UI.Fertilizers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.Fertilizers.Interfaces
{
    public interface IFertilizerQueries : IBaseQueries
    {
        Task<Fertilizer> GetById(int id);
        Task<IEnumerable<Fertilizer>> Gets();
        Task<IEnumerable<Fertilizer>> GetAll();
    }
}
