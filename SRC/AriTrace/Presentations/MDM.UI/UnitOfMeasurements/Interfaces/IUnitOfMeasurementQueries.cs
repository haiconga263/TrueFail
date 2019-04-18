using Common.Interfaces;
using MDM.UI.UnitOfMeasurements.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.UnitOfMeasurements.Interfaces
{
    public interface IUnitOfMeasurementQueries : IBaseQueries
    {
        Task<UOM> GetByIdAsync(int id);

        Task<IEnumerable<UOM>> GetsAsync();
        Task<IEnumerable<UOM>> GetAllAsync();
    }
}
