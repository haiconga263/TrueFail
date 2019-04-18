using DAL;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Order.Commands.RetailerBuyingCalendar
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository = null;
        private readonly IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries = null;
        public DeleteCommandHandler(IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository, IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries)
        {
            this.retailerBuyingCalendarRepository = retailerBuyingCalendarRepository;
            this.retailerBuyingCalendarQueries = retailerBuyingCalendarQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            if(request.BuyingCalendarId == 0)
            {
                return -1;
            }

            var order = await retailerBuyingCalendarQueries.Get(request.BuyingCalendarId);
            if(order != null)
            {
                order = DeleteBuild(order, request.LoginSession);
                return await retailerBuyingCalendarRepository.Update(order);
            }

            return -1;
        }
    }
}
