using Collections.UI.PurchaseOrders.Interfaces;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Collections.UI.PurchaseOrders.Models;
using Collections.UI.PurchaseOrders.ViewModels;
using DAL;
using MDM.UI.Storages.Interfaces;
using MDM.UI.Storages.Enumerations;
using Common.Exceptions;
using MDM.UI.Products.Models;
using Web.Helpers;
using Common;
using Collections.UI.Common;

namespace Collections.PurchaseOrders.Commands
{
    public class InsertPOItemCommand : BaseCommand<object>
    {
        public PurchaseOrderItem PurchaseOrderItem { get; set; }

        public InsertPOItemCommand(PurchaseOrderItem purchaseOrderItem)
        {
            PurchaseOrderItem = purchaseOrderItem;
        }
    }

    public class InsertPOItemCommandHandler : BaseCommandHandler<InsertPOItemCommand, object>
    {
        private readonly IPurchaseOrderService _purchaseOrderService = null;
        private readonly IPurchaseOrderQueries _purchaseOrderQueries = null;

        public InsertPOItemCommandHandler(IPurchaseOrderService purchaseOrderService, IPurchaseOrderQueries purchaseOrderQueries)
        {
            _purchaseOrderService = purchaseOrderService;
            _purchaseOrderQueries = purchaseOrderQueries;
        }

        public override async Task<object> HandleCommand(InsertPOItemCommand request, CancellationToken cancellationToken)
        {
            var product = await WebHelper.HttpGet<Product>(GlobalConfiguration.APIGateWayURI, AppUrls.ApiGetProductById + "?productId=" + request.PurchaseOrderItem.ProductId, request.LoginSession.AccessToken, request.LoginSession.LanguageCode);
            if (request.PurchaseOrderItem.ProductId == 0 || product == null)
            {
                throw new BusinessException("Product.NotExisted");
            }

            var po = await _purchaseOrderQueries.GetByIdAsync(request.PurchaseOrderItem.PurchasingOrderId);
            if (request.PurchaseOrderItem.PurchasingOrderId == 0 || po == null)
            {
                throw new BusinessException("PO.NotExisted");
            }

            int id = await _purchaseOrderService.InsertPOItemAsync(request.PurchaseOrderItem, request.LoginSession);
            return new { PurchaseOrderId = id };
        }
    }
}
