using Collections.UI.PurchaseOrders.Interfaces;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Collections.UI.PurchaseOrders.Models;
using Collections.UI.PurchaseOrders.ViewModels;
using DAL;

namespace Collections.PurchaseOrders.Commands
{
    public class GetPOItemListCommand : BaseCommand<PurchaseOrderInformation>
    {
        public DateTime? Date { get; set; }

        public GetPOItemListCommand(DateTime? date)
        {
            Date = date ?? DateTime.Now.Date;
        }
    }

    public class GetPOItemListCommandHandler : BaseCommandHandler<GetPOItemListCommand, PurchaseOrderInformation>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository = null;
        private readonly IPurchaseOrderQueries _purchaseOrderQueries = null;

        public GetPOItemListCommandHandler(IPurchaseOrderRepository purchaseOrderRepository, IPurchaseOrderQueries purchaseOrderQueries)
        {
            this._purchaseOrderRepository = purchaseOrderRepository;
            this._purchaseOrderQueries = purchaseOrderQueries;
        }

        public override async Task<PurchaseOrderInformation> HandleCommand(GetPOItemListCommand request, CancellationToken cancellationToken)
        {
            PurchaseOrderInformation rs = null;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        rs = await _purchaseOrderQueries.GetByDateCodeAsync(request.Date?.ToString("yyyyMMdd"));
                    }
                    finally
                    {
                        if (rs != null)
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

            return rs;
        }
    }
}
