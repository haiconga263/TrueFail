using Common;
using Common.Exceptions;
using DAL;
using Order.UI;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Order.Commands.FarmerOrder
{
    public class UpdateStatusCommandHandler : BaseCommandHandler<UpdateStatusCommand, int>
    {
        private readonly IFarmerOrderRepository farmerOrderRepository = null;
        private readonly IFarmerOrderQueries farmerOrderQueries = null;
        public UpdateStatusCommandHandler(IFarmerOrderRepository farmerOrderRepository,
                                    IFarmerOrderQueries farmerOrderQueries)
        {
            this.farmerOrderRepository = farmerOrderRepository;
            this.farmerOrderQueries = farmerOrderQueries;
        }
        public override async Task<int> HandleCommand(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderId == 0)
            {
                return -1;
            }

            if(!Enum.IsDefined(typeof(FarmerOrderStatuses), request.StatusId))
            {
                return -1; //wrong status
            }

            var order = await farmerOrderQueries.Get(request.OrderId);
            if(order != null)
            {
                if (request.StatusId == (int)FarmerOrderStatuses.Canceled)
                {
                    //warning: condition of cancel FarmerOrder
                    if(order.BuyingDate > DateTime.Now.Date)
                    {
                        throw new BusinessException("Order.Farmer.ErrorCanceledCondition");
                    }
                }
                else
                {
                    if (order.StatusId != request.StatusId - 1)
                    {
                        throw new BusinessException("Order.WrongStep");
                    }
                }

                order = UpdateBuild(order, request.LoginSession);
                order.StatusId = request.StatusId;
                return await farmerOrderRepository.Update(order);
            }

            return -1;
        }
    }
}
