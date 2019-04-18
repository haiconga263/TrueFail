using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Retailers.Models;
using Order.UI;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Order.Commands.RetailerOrder
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IRetailerOrderRepository retailerOrderRepository = null;
        private readonly IRetailerOrderQueries retailerOrderQueries = null;
        public DeleteCommandHandler(IRetailerOrderRepository retailerOrderRepository,
                                    IRetailerOrderQueries retailerOrderQueries)
        {
            this.retailerOrderRepository = retailerOrderRepository;
            this.retailerOrderQueries = retailerOrderQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderId == 0)
            {
                return -1;
            }

            var retailerId = -1;
            if (request.LoginSession.Roles.FirstOrDefault(r => r == "Administrator") == null)
            {
                var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={request.LoginSession.Id}", request.LoginSession.AccessToken);
                if (retailer == null)
                {
                    throw new BusinessException("Retailer.NotExisted");
                }
                retailerId = retailer.Id;
            }

            var order = await retailerOrderQueries.Get(request.OrderId);
            if(order != null)
            {
                if(order.CreatedDate.AddSeconds(GlobalConfiguration.TimeoutOfRetailerOrder) < DateTime.Now)
                {
                    return -1; //Time out
                }

                if (retailerId != -1 && retailerId != order.RetailerId)
                {
                    throw new NotPermissionException();
                }

                order = DeleteBuild(order, request.LoginSession);
                return await retailerOrderRepository.Update(order);
            }

            return -1;
        }
    }
}
