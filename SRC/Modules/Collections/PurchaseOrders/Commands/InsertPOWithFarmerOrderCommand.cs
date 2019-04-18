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
using Order.UI.ViewModels;

namespace Collections.PurchaseOrders.Commands
{
    public class InsertPOWithFarmerOrderCommand : BaseCommand<object>
    {
        public FarmerOrderViewModel FarmerOrder { get; set; }

        public InsertPOWithFarmerOrderCommand(FarmerOrderViewModel farmerOrder)
        {
            FarmerOrder = farmerOrder;
        }
    }

    public class InsertPOWithFarmerOrderCommandHandler : BaseCommandHandler<InsertPOWithFarmerOrderCommand, object>
    {

        private readonly IPurchaseOrderService _purchaseOrderService = null;
        private readonly IPurchaseOrderQueries _purchaseOrderQueries = null;

        public InsertPOWithFarmerOrderCommandHandler(IPurchaseOrderService purchaseOrderService, IPurchaseOrderQueries purchaseOrderQueries)
        {
            _purchaseOrderService = purchaseOrderService;
            _purchaseOrderQueries = purchaseOrderQueries;
        }

        public override async Task<object> HandleCommand(InsertPOWithFarmerOrderCommand request, CancellationToken cancellationToken)
        {
            int poId = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        poId = await _purchaseOrderService.InsertPOWithFarmerOrderAsync(request.FarmerOrder, request.LoginSession);
                    }
                    finally
                    {
                        if (poId > 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                        }
                    }
                }
            }

            return new { PurchaseOrderId = poId };
        }
    }
}
