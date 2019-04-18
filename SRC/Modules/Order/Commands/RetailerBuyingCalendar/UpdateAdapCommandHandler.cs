using Common.Exceptions;
using DAL;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Order.Commands.RetailerBuyingCalendar
{
    public class UpdateAdapCommandHandler : BaseCommandHandler<UpdateAdapCommand, int>
    {
        private readonly IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository = null;
        private readonly IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries = null;
        public UpdateAdapCommandHandler(IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository, IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries)
        {
            this.retailerBuyingCalendarRepository = retailerBuyingCalendarRepository;
            this.retailerBuyingCalendarQueries = retailerBuyingCalendarQueries;
        }
        public override async Task<int> HandleCommand(UpdateAdapCommand request, CancellationToken cancellationToken)
        {
            if(request.BuyingCalendar == null || request.BuyingCalendar.Id == 0)
            {
                return -1;
            }

            var rs = 0;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        retailerBuyingCalendarRepository.JoinTransaction(conn, trans);
                        retailerBuyingCalendarQueries.JoinTransaction(conn, trans);

                        var order = await retailerBuyingCalendarQueries.Get(request.BuyingCalendar.Id);
                        if (order == null)
                        {
                            throw new BusinessException("Planning.NotExisted");
                        }

                        order.IsAdaped = true;

                        foreach (var item in request.BuyingCalendar.Items)
                        {
                            var checkingItem = order.Items.First(i => i.Id == item.Id);
                            checkingItem.AdapQuantity = item.AdapQuantity;
                            await retailerBuyingCalendarRepository.UpdateItem(checkingItem);
                            order.IsAdaped = order.IsAdaped && checkingItem.AdapQuantity >= checkingItem.Quantity;
                        }

                        order.AdapNote = request.BuyingCalendar.AdapNote;
                        order = UpdateBuild(order, request.LoginSession);
                        if (await retailerBuyingCalendarRepository.Update(order) != 0)
                        {
                            throw new BusinessException("RetailerPlanning.AdapFailed");
                        }
                        
                    }
                    catch(Exception ex)
                    {
                        rs = -1;
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                            // notify
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
