using Collections.UI.PurchaseOrders.Models;
using Collections.UI.PurchaseOrders.ViewModels;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collections.UI.PurchaseOrders.Interfaces
{
    public interface IPurchaseOrderQueries : IBaseQueries
    {
        Task<PurchaseOrderInformation> GetByIdAsync(long id);
        Task<PurchaseOrderItem> GetItemByItemIdAsync(long itemId);
        Task<PurchaseOrderInformation> GetByDateCodeAsync(string datecode);
        Task<IEnumerable<PurchaseOrder>> GetsAsync();
        Task<IEnumerable<PurchaseOrder>> GetsAsync(DateTime lastRequest);
    }
}
