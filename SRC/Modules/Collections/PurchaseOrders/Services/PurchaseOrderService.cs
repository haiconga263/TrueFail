using Collections.UI.PurchaseOrders.Interfaces;
using Collections.UI.PurchaseOrders.Models;
using Common.Models;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Services;

namespace Collections.PurchaseOrders.Services
{
    public class PurchaseOrderService : BaseService, IPurchaseOrderService
    {
        private readonly IStorageQueries _storageQueries = null;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository = null;
        private readonly IPurchaseOrderQueries _purchaseOrderQueries = null;
        public PurchaseOrderService(IStorageQueries storageQueries,
                IPurchaseOrderRepository purchaseOrderRepository, IPurchaseOrderQueries purchaseOrderQueries)
        {
            _storageQueries = storageQueries;
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderQueries = purchaseOrderQueries;
        }

        public async Task<int> InsertPOAsync(DateTime dateRequest, UserSession userSession)
        {
            var po = new PurchaseOrder();
            po.Code = await _storageQueries.GenarateCodeAsync(StorageKeys.PurchaseOrderCode);
            po.DateCode = dateRequest.ToString("yyyyMMdd");
            po.IsDeleted = false;
            po.CreatedBy = userSession.Id;
            po.CreatedDate = DateTime.Now;
            po.ModifiedBy = userSession.Id;
            po.ModifiedDate = DateTime.Now;
            return await _purchaseOrderRepository.AddAsync(po);
        }

        public async Task<int> InsertPOItemAsync(PurchaseOrderItem poItem, UserSession userSession)
        {
            poItem.IsDeleted = false;
            poItem.CreatedBy = userSession.Id;
            poItem.CreatedDate = DateTime.Now;
            poItem.ModifiedBy = userSession.Id;
            poItem.ModifiedDate = DateTime.Now;
            return await _purchaseOrderRepository.AddItemAsync(poItem);
        }

        public async Task<int> InsertPOWithFarmerOrderAsync(FarmerOrderViewModel farmerOrder, UserSession userSession)
        {
            var poId = await InsertPOAsync(farmerOrder.BuyingDate, userSession);
            foreach (var orItem in farmerOrder.Items)
            {
                var poItem = new PurchaseOrderItem()
                {
                    PriceLimit = 0,
                    PriceRecommend = orItem.Price,
                    ProductId = orItem.ProductId,
                    PurchasingOrderId = poId,
                    StatusCode = UI.PurchaseOrders.Enumerations.PurchaseOrderStatus.purchasing,
                    Quantity = orItem.OrderedQuantity,
                    UomName = orItem.UoM?.Name ?? "",
                };

                var itemId = await InsertPOItemAsync(poItem, userSession);
            }

            return poId;
        }
    }
}
