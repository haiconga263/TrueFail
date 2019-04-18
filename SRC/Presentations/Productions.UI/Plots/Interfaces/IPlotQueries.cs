using Common.Interfaces;
using Productions.UI.Plots.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Productions.UI.Plots.Interfaces
{
    public interface IPlotQueries : IBaseQueries
    {
        Task<Plot> GetById(int id);
        Task<IEnumerable<Plot>> Gets();
    }
}
