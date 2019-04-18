using Common.Interfaces;
using Productions.UI.Plots.Models;
using System.Threading.Tasks;

namespace Productions.UI.Plots.Interfaces
{
    public interface IPlotRepository : IBaseRepository
    {
        Task<int> Add(Plot plot);
        Task<int> Update(Plot plot);
        Task<int> Delete(int id);
    }
}
