using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Retailers.Models;
using Order.UI;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Order.Commands.RetailerOrder
{
    public class UpdatTripCommandHandler : BaseCommandHandler<UpdateTripCommand, int>
    {
        private readonly IRetailerOrderRepository retailerOrderRepository = null;
        private readonly IRetailerOrderQueries retailerOrderQueries = null;
        public UpdatTripCommandHandler(IRetailerOrderRepository retailerOrderRepository,
                                    IRetailerOrderQueries retailerOrderQueries)
        {
            this.retailerOrderRepository = retailerOrderRepository;
            this.retailerOrderQueries = retailerOrderQueries;
        }
        public override async Task<int> HandleCommand(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderId == 0)
            {
                throw new BusinessException("Order.NotExisted");
            }

            var order = await retailerOrderQueries.Get(request.OrderId);

            if(order == null)
            {
                throw new BusinessException("Order.NotExisted");
            }

            order.TripId = request.TripId;
            order = UpdateBuild(order, request.LoginSession);
            return await retailerOrderRepository.Update(order);
        }
    }
}
