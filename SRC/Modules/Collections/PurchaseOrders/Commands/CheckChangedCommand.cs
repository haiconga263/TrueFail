using DAL;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Collections.UI.PurchaseOrders.Interfaces;
using System;
using System.Linq;
using Collections.UI.PurchaseOrders.ViewModels;
using Collections.UI.POActivities.Interfaces;
using Collections.UI.POActivities.ViewModels;
using System.Collections.Generic;

namespace Collections.PurchaseOrders.Commands
{
    public class CheckChangedCommand : BaseCommand<PurchaseOrderChangedInfomation>
    {
        public DateTime? LastRequest { get; set; }

        public CheckChangedCommand(DateTime? lastRequest)
        {
            LastRequest = lastRequest ?? DateTime.Now.Date;
        }
    }

    public class CheckChangedCommandHandler : BaseCommandHandler<CheckChangedCommand, PurchaseOrderChangedInfomation>
    {
        private readonly IPurchaseOrderQueries _purchaseOrderQueries = null;
        private readonly IPOActivityQueries _poActivityQueries = null;

        public CheckChangedCommandHandler(IPurchaseOrderQueries purchaseOrderQueries, IPOActivityQueries poActivityQueries)
        {
            _purchaseOrderQueries = purchaseOrderQueries;
            _poActivityQueries = poActivityQueries;
        }
        public override async Task<PurchaseOrderChangedInfomation> HandleCommand(CheckChangedCommand request, CancellationToken cancellationToken)
        {
            var rs = new PurchaseOrderChangedInfomation()
            {
                IsChanged = false,
                LastChanged = request.LastRequest.Value,
                Activities = new List<POActivityInformation>()
            };

            var po = await _purchaseOrderQueries.GetsAsync(request.LastRequest.Value);

            if (po != null && po.Count() > 0)
            {
                rs.LastChanged = po.Max(x => x.CreatedDate);
                rs.IsChanged = true;
            }

            var activities = await _poActivityQueries.GetsAsync(request.LastRequest.Value);
            if (activities != null && activities.Count() > 0)
            {
                var lastChanged = activities.Max(x => x.CreatedDate);
                if (lastChanged > rs.LastChanged)
                    rs.LastChanged = lastChanged;
                rs.IsChanged = true;
                rs.Activities = activities;
            }

            return rs;
        }
    }
}
