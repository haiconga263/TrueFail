using Common.Interfaces;
using Productions.UI.Fertilizers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.Fertilizers.Interfaces
{
    public interface IFertilizerCategoryQueries : IBaseQueries
    {
        Task<FertilizerCategory> GetById(int id);
        Task<IEnumerable<FertilizerCategory>> Gets();
        Task<IEnumerable<FertilizerCategory>> GetAll();
    }
}
