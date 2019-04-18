using Collections.UI.PurchaseOrders.Models;
using Common.Interfaces;
using Common.Models;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Collections.UI.PurchaseOrders.Interfaces
{
    public interface IPurchaseOrderService : IBaseService
    {
        Task<int> InsertPOAsync(DateTime dateRequest, UserSession userSession);
        Task<int> InsertPOItemAsync(PurchaseOrderItem poItem, UserSession userSession);
        Task<int> InsertPOWithFarmerOrderAsync(FarmerOrderViewModel farmerOrder, UserSession userSession);
    }
}
