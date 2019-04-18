using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Retailers.Models;
using Newtonsoft.Json;
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

namespace Order.Commands.RetailerBuyingCalendar
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository = null;
        private readonly IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries = null;
        public AddCommandHandler(IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository,
                                 IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries = null)
        {
            this.retailerBuyingCalendarRepository = retailerBuyingCalendarRepository;
            this.retailerBuyingCalendarQueries = retailerBuyingCalendarQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if(request.BuyingCalendar == null || request.BuyingCalendar.Items == null || 
               request.BuyingCalendar.Items.Count == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={request.LoginSession.Id}", request.LoginSession.AccessToken);
            if(retailer == null)
            {
                throw new BusinessException("Retailer.NotExisted");
            }

            request.BuyingCalendar.RetailerId = retailer.Id;

            var checkingPlanning = (await retailerBuyingCalendarQueries.Gets($"o.code = '{request.BuyingCalendar.Code}' AND o.retailer_id = {request.BuyingCalendar.RetailerId}")).FirstOrDefault();
            if(checkingPlanning != null)
            {
                throw new BusinessException("Order.Retailer.Planning.ExistedCode");
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
                        request.BuyingCalendar.Code = await retailerBuyingCalendarQueries.GenarateCode();
                        request.BuyingCalendar = CreateBuild(request.BuyingCalendar, request.LoginSession);
                        request.BuyingCalendar.IsExpired = request.BuyingCalendar.BuyingDate <= DateTime.Now ? true : false;
                        request.BuyingCalendar.IsOrdered = false;
                        request.BuyingCalendar.IsAdaped = true;
                        request.BuyingCalendar.AdapNote = string.Empty;
                        var id = await retailerBuyingCalendarRepository.Add(request.BuyingCalendar);
                        foreach (var item in request.BuyingCalendar.Items)
                        {
                            item.RetailerBuyingCalendarId = id;
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
