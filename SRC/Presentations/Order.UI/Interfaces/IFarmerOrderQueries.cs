using Common.Interfaces;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IFarmerOrderQueries : IBaseQueries
    {
        Task<string> GenarateCode();
        Task<IEnumerable<FarmerOrderViewModel>> Gets(int farmerId);
        Task<IEnumerable<FarmerOrderViewModel>> Gets(string condition = "");
        Task<FarmerOrderViewModel> Get(long id);
        Task<bool> CheckChangedByFarmer(int farmerId, DateTime lastRequest);
    }
}
