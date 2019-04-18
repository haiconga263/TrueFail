using Common;
using DAL;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Order.Commands.FarmerOrder
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

            var order = await retailerOrderQueries.Get(request.OrderId);
            if(order != null)
            {
                #warning Hoang Uncompleted
                // check business rule here

                order = DeleteBuild(order, request.LoginSession);
                return await retailerOrderRepository.Update(order);
            }

            return -1;
        }
    }
}
