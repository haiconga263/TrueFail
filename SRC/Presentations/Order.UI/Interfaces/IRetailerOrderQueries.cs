using Common.Interfaces;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.UI.Interfaces
{
    public interface IRetailerOrderQueries : IBaseQueries
    {
        Task<IEnumerable<RetailerOrderViewModel>> Gets(int retailer);
        Task<IEnumerable<RetailerOrderViewModel>> Gets(string condition = "");
        Task<RetailerOrderViewModel> Get(long id);
        Task<RetailerOrderViewModel> GetByCode(string code);
        Task<string> GenarateCode();


        Task<IEnumerable<RetailerOrderAuditViewModel>> GetAudits(long retailerOrderId, int retailerId);
    }
}
