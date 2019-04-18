using Common.Interfaces;
using MDM.UI.UnitOfMeasurements.Models;
using System.Threading.Tasks;

namespace MDM.UI.UnitOfMeasurements.Interfaces
{
    public interface IUnitOfMeasurementRepository: IBaseRepository
    {
        Task<int> AddAsync(UOM uom);
        Task<int> UpdateAsync(UOM uom);
        Task<int> DeleteAsync(int id);
    }
}
