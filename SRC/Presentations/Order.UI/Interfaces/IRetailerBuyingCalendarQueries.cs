using Common.Interfaces;
using Order.UI.Models;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IRetailerBuyingCalendarQueries : IBaseQueries
    {
        Task<string> GenarateCode();
        Task<IEnumerable<RetailerBuyingCalendarViewModel>> Gets(int retailer);
        Task<IEnumerable<RetailerBuyingCalendarViewModel>> Gets(string condition = "");
        Task<RetailerBuyingCalendarViewModel> Get(long id);
    }
}
