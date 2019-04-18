using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.ViewModels;
using Order.UI;
using Order.UI.Interfaces;
using Order.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Order.Commands.FarmerOrder
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IFarmerOrderRepository farmerOrderRepository = null;
        private readonly IFarmerOrderQueries farmerOrderQueries = null;
        private readonly IFarmerBuyingCalendarRepository farmerBuyingCalendarRepository = null;
        private readonly IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries = null;
        private readonly IFarmerRetailerOrderItemRepository farmerRetailerOrderItemRepository = null;
        private readonly IFarmerRetailerOrderItemQueries farmerRetailerOrderItemQueries = null;
        private readonly IProductQueries productQueries = null;
        public AddCommandHandler(IFarmerOrderRepository farmerOrderRepository,
                                 IFarmerOrderQueries farmerOrderQueries,
                                 IFarmerBuyingCalendarRepository farmerBuyingCalendarRepository,
                                 IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries,
                                 IFarmerRetailerOrderItemRepository farmerRetailerOrderItemRepository,
                                 IFarmerRetailerOrderItemQueries farmerRetailerOrderItemQueries,
                                 IProductQueries productQueries = null)
        {
            this.farmerOrderRepository = farmerOrderRepository;
            this.farmerOrderQueries = farmerOrderQueries;
            this.farmerBuyingCalendarRepository = farmerBuyingCalendarRepository;
            this.farmerBuyingCalendarQueries = farmerBuyingCalendarQueries;
            this.farmerRetailerOrderItemRepository = farmerRetailerOrderItemRepository;
            this.farmerRetailerOrderItemQueries = farmerRetailerOrderItemQueries;
            this.productQueries = productQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Order == null || request.Order.FarmerId == 0
               || request.Order.Items == null || request.Order.Items.Count == 0)
            {
                return -1;
            }

            var rs = 0;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        farmerOrderRepository.JoinTransaction(conn, trans);
                        farmerOrderQueries.JoinTransaction(conn, trans);
                        productQueries.JoinTransaction(conn, trans);

                        IEnumerable<FarmerRetailerOrderItems> farmerRetailerItems = null;

                        if (request.Order.FarmerBuyingCalendarId == 0) request.Order.FarmerBuyingCalendarId = null;

                        // update status for buying calendar
                        if (request.Order.FarmerBuyingCalendarId != null)
                        {
                            farmerBuyingCalendarQueries.JoinTransaction(conn, trans);
                            farmerRetailerOrderItemQueries.JoinTransaction(conn, trans);
                            var buyingCalendar = await farmerBuyingCalendarQueries.Get(request.Order.FarmerBuyingCalendarId.Value);
                            if (buyingCalendar != null)
                            {
                                farmerBuyingCalendarRepository.JoinTransaction(conn, trans);
                                buyingCalendar = UpdateBuild(buyingCalendar, request.LoginSession);
                                buyingCalendar.IsOrdered = true;
                                var rsUpdate = await farmerBuyingCalendarRepository.Update(buyingCalendar);
                                if (rs != 0)
                                {
                                    throw new Exception("Update Buying calendar failed");
                                }
                                farmerRetailerItems = await farmerRetailerOrderItemQueries.GetByBC(buyingCalendar.Id);
                            }
                        }

                        request.Order.Code = await farmerOrderQueries.GenarateCode();
                        request.Order = CreateBuild(request.Order, request.LoginSession);
                        request.Order.StatusId = (int)UI.FarmerOrderStatuses.BeginOrder;
                        request.Order.Id = await farmerOrderRepository.Add(request.Order);
                        decimal totalAmount = 0;
                        foreach (var item in request.Order.Items)
                        {
                            var prod = (await WebHelper.HttpGet<IEnumerable<ProductViewModel>>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetProduct}?productId={item.ProductId}", request.LoginSession.AccessToken)).FirstOrDefault(p => p.CurrentUoM == item.UoMId);
                            if (prod == null)
                            {
                                throw new Exception("Product doesn't existed");
                            }

                            item.FarmerOrderId = request.Order.Id;
                            item.StatusId = (int)UI.FarmerOrderStatuses.BeginOrder;
                            item.DeliveriedQuantity = 0;
                            item.Price = prod.BuyingCurrentPrice;
                            var itemId = await farmerOrderRepository.AddItem(item);

                            // Mapping From FarmerRetailer BuyingCalendar to FarmerRetailer Order
                            if (farmerRetailerItems != null)
                            {
                                var FRItems = farmerRetailerItems.Where(i => i.ProductId == item.ProductId && i.UoMId == item.UoMId);
                                int totalQuantity = item.DeliveriedQuantity;
                                foreach (var FRItem in FRItems)
                                {
                                    await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                    {
                                        IsPlanning = false,
                                        FarmerId = FRItem.FarmerId,
                                        FarmerOrderId = FRItem.FarmerOrderId,
                                        FarmerOrderItemId = FRItem.FarmerOrderItemId,
                                        RetailerId = FRItem.RetailerId,
                                        RetailerOrderId = FRItem.RetailerOrderId,
                                        RetailerOrderItemId = FRItem.RetailerOrderItemId,
                                        ProductId = item.ProductId,
                                        Quantity = (totalQuantity < FRItem.Quantity) ? totalQuantity : FRItem.Quantity,
                                        UoMId = item.UoMId
                                    });
                                    totalQuantity = (totalQuantity - FRItem.Quantity < 0) ? 0 : totalQuantity - FRItem.Quantity;
                                }
                            }

                            totalAmount += prod.BuyingCurrentPrice * item.OrderedQuantity;
                        }

                        request.Order.TotalAmount = totalAmount;
                        if (await farmerOrderRepository.Update(request.Order) != 0)
                        {
                            throw new Exception("Update Order failed");
                        }
                    }
                    catch (Exception ex)
                    {
                        rs = -1;
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0)
                            trans.Commit();
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

            return 0;
        }
    }
}
