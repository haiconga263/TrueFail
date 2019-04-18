using Abivin.Integration.UI.ViewModels;
using Common.Interfaces;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abivin.Integration.UI.Interfaces
{
    public interface IAbivinOrderQueries : IBaseQueries
    {
        Task<IEnumerable<OrderViewModel>> Get(string code);
        Task<IEnumerable<OrderViewModel>> Gets(string condition = "");
        Task<RetailerOrderViewModel> GetByCode(string code);
    }
}
