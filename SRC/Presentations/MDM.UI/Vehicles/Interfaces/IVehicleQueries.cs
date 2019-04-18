using Common.Interfaces;
using MDM.UI.Vehicles.Models;
using MDM.UI.Vehicles.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Vehicles.Interfaces
{
    public interface IVehicleQueries : IBaseQueries
    {
        Task<VehicleViewModel> Get(int id);
        Task<IEnumerable<VehicleViewModel>> Gets(string condition = "");

        Task<VehicleType> GetType(int id);
        Task<IEnumerable<VehicleType>> GetTypes(string condition = "");

        Task<string> GenarateCode();
    }
}
