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

namespace Order.Commands.RetailerBuyingCalendar
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository = null;
        private readonly IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries = null;
        public UpdateCommandHandler(IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository, IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries)
        {
            this.retailerBuyingCalendarRepository = retailerBuyingCalendarRepository;
            this.retailerBuyingCalendarQueries = retailerBuyingCalendarQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.BuyingCalendar == null || request.BuyingCalendar.Id == 0 
               || request.BuyingCalendar.Items == null || request.BuyingCalendar.Items.Count == 0)
            {
                throw new BusinessException("Order.Retailer.Planning.NotExisted");
            }

            var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={request.LoginSession.Id}", request.LoginSession.AccessToken);
            if (retailer == null)
            {
                throw new BusinessException("Retailer.NotExisted");
            }

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

                        //It's not a owner planning.
                        if(retailer.Id != order.RetailerId || retailer.Id != request.BuyingCalendar.RetailerId)
                        {
                            throw new NotPermissionException();
                        }

                        request.BuyingCalendar = UpdateBuild(request.BuyingCalendar, request.LoginSession);
                        request.BuyingCalendar.Code = order.Code;
                        request.BuyingCalendar.CreatedDate = order.CreatedDate;
                        request.BuyingCalendar.CreatedBy = order.CreatedBy;
                        request.BuyingCalendar.IsExpired = request.BuyingCalendar.BuyingDate <= DateTime.Now ? true : false;
                        request.BuyingCalendar.IsAdaped = true;
                        request.BuyingCalendar.AdapNote = string.Empty;
                        request.BuyingCalendar.Code = order.Code;
                        await retailerBuyingCalendarRepository.Update(request.BuyingCalendar);
                        await retailerBuyingCalendarRepository.DeleteItems(request.BuyingCalendar.Id);
                        foreach (var item in request.BuyingCalendar.Items)
                        {
                            item.RetailerBuyingCalendarId = request.BuyingCalendar.Id;
                            item.AdapQuantity = item.Quantity;
                            await retailerBuyingCalendarRepository.AddItem(item);
                        }

                        trans.Commit();
                    }
                    catch(Exception ex)
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch { }
                        throw ex;
                    }
                }
            }

           return 0;
        }
    }
}
