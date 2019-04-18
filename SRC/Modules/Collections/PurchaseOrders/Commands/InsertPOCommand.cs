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

namespace Collections.PurchaseOrders.Commands
{
    public class InsertPOCommand : BaseCommand<object>
    {
        public DateTime? DateRequest { get; set; }

        public InsertPOCommand(DateTime? dateRequest)
        {
            DateRequest = dateRequest ?? DateTime.Now;
        }
    }

    public class InsertPOCommandHandler : BaseCommandHandler<InsertPOCommand, object>
    {

        private readonly IPurchaseOrderService _purchaseOrderService = null;

        public InsertPOCommandHandler(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        public override async Task<object> HandleCommand(InsertPOCommand request, CancellationToken cancellationToken)
        {
            int id = await _purchaseOrderService.InsertPOAsync(request.DateRequest ?? DateTime.Now, request.LoginSession);
            return new { PurchaseOrderId = id };
        }
    }
}
