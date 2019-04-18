using Collections.UI.Common;
using Collections.UI.POActivities.Interfaces;
using Collections.UI.POActivities.ViewModels;
using Collections.UI.PurchaseOrders.Enumerations;
using Collections.UI.PurchaseOrders.Interfaces;
using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Farmers.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Collections.POActivities.Commands
{
    public class InsertActivityCommand : BaseCommand<object>
    {
        public POActivityViewModel Activity { get; set; }

        public InsertActivityCommand(POActivityViewModel activity)
        {
            Activity = activity;
        }
    }

    public class InsertActivityCommandHandler : BaseCommandHandler<InsertActivityCommand, object>
    {
        private readonly IPurchaseOrderQueries _purchaseOrderQueries = null;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository = null;

        private readonly IPOActivityQueries _poActivityQueries = null;
        private readonly IPOActivityRepository _poActivityRepository = null;

        public InsertActivityCommandHandler(IPurchaseOrderQueries purchaseOrderQueries, IPurchaseOrderRepository purchaseOrderRepository,
                                            IPOActivityQueries poActivityQueries, IPOActivityRepository poActivityRepository)
        {
            _purchaseOrderQueries = purchaseOrderQueries;
            _purchaseOrderRepository = purchaseOrderRepository;
            _poActivityQueries = poActivityQueries;
            _poActivityRepository = poActivityRepository;
        }
        public override async Task<object> HandleCommand(InsertActivityCommand request, CancellationToken cancellationToken)
        {
            var farmer = await WebHelper.HttpGet<Farmer>(GlobalConfiguration.APIGateWayURI, AppUrls.ApiGetFarmer + "?farmerId=" + request.Activity.FarmerId, request.LoginSession.AccessToken);
            if (request.Activity.FarmerId == 0 || farmer == null)
            {
                throw new BusinessException("Farmer.NotExisted");
            }

            var poItem = await _purchaseOrderQueries.GetItemByItemIdAsync(request.Activity.PurchaseOrderItemId);
            if (request.Activity.PurchaseOrderItemId == 0 || poItem == null)
            {
                throw new BusinessException("POItem.NotExisted");
            }

            if (poItem.StatusCode == PurchaseOrderStatus.canceled)
            {
                throw new BusinessException("POItem.Cancelled");
            }

            if (poItem.StatusCode == PurchaseOrderStatus.completed)
            {
                throw new BusinessException("POItem.Completed");
            }

            int actId = 0;
            bool rsUdpPOItem = false;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Activity.CollectorId = request.LoginSession.Id;
                        request.Activity.PurchaseDate = DateTime.Now;
                        request.Activity.IsDeleted = false;
                        request.Activity.CreatedBy = request.LoginSession.Id;
                        request.Activity.CreatedDate = DateTime.Now;
                        request.Activity.ModifiedBy = request.LoginSession.Id;
                        request.Activity.ModifiedDate = DateTime.Now;

                        actId = await _poActivityRepository.AddAsync(request.Activity);

                        poItem.QuantityPurchased += request.Activity.Quantity;
                        if (poItem.QuantityPurchased >= poItem.Quantity)
                            poItem.StatusCode = PurchaseOrderStatus.completed;
                        poItem.ModifiedBy = request.LoginSession.Id;
                        poItem.ModifiedDate = DateTime.Now;

                        rsUdpPOItem = await _purchaseOrderRepository.UpdateItemAsync(poItem);

                        // push notice
                    }
                    finally
                    {
                        if (actId > 0 && rsUdpPOItem == true)
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

            return new { POActivityActivityId = actId };
        }
    }
}
