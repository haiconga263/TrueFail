using Common.Interfaces;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IFarmerBuyingCalendarQueries : IBaseQueries
    {
        Task<IEnumerable<FarmerBuyingCalendarViewModel>> Gets(int farmerId);
        Task<IEnumerable<FarmerBuyingCalendarViewModel>> Gets(string condition = "");
        Task<FarmerBuyingCalendarViewModel> Get(long id);

        Task<string> GenarateCode();
        Task<bool> CheckChangedByFarmer(int v, DateTime lastRequest);
    }
}
