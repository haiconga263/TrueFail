using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
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
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IRetailerOrderRepository retailerOrderRepository = null;
        private readonly IRetailerOrderQueries retailerOrderQueries = null;
        private readonly IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository = null;
        private readonly IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries = null;
        public AddCommandHandler(IRetailerOrderRepository retailerOrderRepository,
                                 IRetailerOrderQueries retailerOrderQueries,
                                 IRetailerBuyingCalendarRepository retailerBuyingCalendarRepository,
                                 IRetailerBuyingCalendarQueries retailerBuyingCalendarQueries)
        {
            this.retailerOrderRepository = retailerOrderRepository;
            this.retailerOrderQueries = retailerOrderQueries;
            this.retailerBuyingCalendarRepository = retailerBuyingCalendarRepository;
            this.retailerBuyingCalendarQueries = retailerBuyingCalendarQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if(request.Order == null || request.Order.Items == null || request.Order.Items.Count == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if(request.LoginSession.Roles.FirstOrDefault(r => r == "Administrator") == null)
            {
                var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={request.LoginSession.Id}", request.LoginSession.AccessToken);
                if (retailer == null)
                {
                    throw new BusinessException("Retailer.NotExisted");
                }
                request.Order.RetailerId = retailer.Id;
            }
            else
            {
                if(request.Order.RetailerId == 0)
                {
                    throw new BusinessException("AddWrongInformation");
                }
                else
                {
                    var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerById}?retailerId={request.Order.RetailerId}", request.LoginSession.AccessToken);
                    if (retailer == null)
                    {
                        throw new BusinessException("Retailer.NotExisted");
                    }
                }
            }

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        retailerOrderRepository.JoinTransaction(conn, trans);

                        // update status for buying calendar
                        if(request.Order.RetailerBuyingCalendarId != null)
                        {
                            retailerBuyingCalendarQueries.JoinTransaction(conn, trans);
                            var buyingCalendar = await retailerBuyingCalendarQueries.Get(request.Order.RetailerBuyingCalendarId.Value);
                            if(buyingCalendar != null)
                            {
                                retailerBuyingCalendarRepository.JoinTransaction(conn, trans);
                                buyingCalendar = UpdateBuild(buyingCalendar, request.LoginSession);
                                buyingCalendar.IsOrdered = true;
                                var rs = await retailerBuyingCalendarRepository.Update(buyingCalendar);
                                if(rs != 0)
                                {
                                    throw new Exception("Update Buying calendar failed");
                                }
                            }
                        }
                        
                        request.Order = CreateBuild(request.Order, request.LoginSession);
                        request.Order.StatusId = (int)RetailerOrderStatuses.Ordered;

                        //Genarate Code
                        request.Order.Code = await retailerOrderQueries.GenarateCode();

                        request.Order.Id = await retailerOrderRepository.Add(request.Order);
                        decimal totalAmount = 0;
                        foreach (var item in request.Order.Items)
                        {
                            var prod = (await WebHelper.HttpGet<IEnumerable<ProductViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetProduct}?productId={item.ProductId}", request.LoginSession.AccessToken)).FirstOrDefault(p => p.CurrentUoM == item.UoMId);
                            if (prod == null)
                            {
                                throw new BusinessException("Product.NotExisted");
                            }

                            item.RetailerOrderId = request.Order.Id;
                            item.StatusId = (int)RetailerOrderStatuses.Ordered;
                            item.AdapQuantity = 0;
                            item.DeliveriedQuantity = 0;
                            item.Price = prod.Prices.FirstOrDefault(p => p.UoMId == item.UoMId).SellingPrice;
                            await retailerOrderRepository.AddItem(item);

                            totalAmount += item.Price * item.OrderedQuantity;
                        }

                        request.Order.TotalAmount = totalAmount;
                        if(await retailerOrderRepository.Update(request.Order) != 0)
                        {
                            throw new Exception("Update Order failed");
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
