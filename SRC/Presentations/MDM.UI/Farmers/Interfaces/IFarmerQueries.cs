using Common.Interfaces;
using MDM.UI.Farmers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Farmers.Interfaces
{
    public interface IFarmerQueries : IBaseQueries
    {
        Task<IEnumerable<FarmerViewModel>> Gets(string condition = "");
        Task<FarmerViewModel> Get(int id);
        Task<FarmerViewModel> GetByUser(int userId);
    }
}
