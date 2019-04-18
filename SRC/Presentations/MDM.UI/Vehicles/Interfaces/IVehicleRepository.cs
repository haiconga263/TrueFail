using Common.Interfaces;
using MDM.UI.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Vehicles.Interfaces
{
    public interface IVehicleRepository : IBaseRepository
    {
        Task<int> Add(Vehicle vehicle);
        Task<int> Update(Vehicle vehicle);
        Task<int> Delete(Vehicle vehicle);
    }
}
