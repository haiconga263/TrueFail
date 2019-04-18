using Common.Exceptions;
using DAL;
using MDM.UI.Products.Interfaces;
using MDM.UI.UoMs.Interfaces;
using Order.UI;
using Order.UI.Interfaces;
using Order.UI.Models;
using Order.UI.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Order.Commands.RetailerOrder
{
    public class ProcessCommandHandler : BaseCommandHandler<ProcessCommand, int>
    {
        private readonly IFarmerBuyingCalendarRepository farmerBuyingCalendarRepository = null;
        private readonly IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries = null;
        private readonly IFarmerRetailerOrderItemRepository farmerRetailerOrderItemRepository = null;
        private readonly IRetailerOrderQueries retailerOrderQueries = null;
        private readonly IRetailerOrderRepository retailerOrderRepository = null;
        private readonly IProductQueries productQueries = null;
        private readonly IUoMQueries uoMQueries = null;
        public ProcessCommandHandler(IFarmerBuyingCalendarRepository farmerBuyingCalendarRepository, 
                                     IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries,
                                     IFarmerRetailerOrderItemRepository farmerRetailerOrderItemRepository,
                                     IRetailerOrderQueries retailerOrderQueries,
                                     IRetailerOrderRepository retailerOrderRepository,
                                     IProductQueries productQueries,
                                     IUoMQueries uoMQueries)
        {
            this.farmerBuyingCalendarRepository = farmerBuyingCalendarRepository;
            this.farmerBuyingCalendarQueries = farmerBuyingCalendarQueries;
            this.farmerRetailerOrderItemRepository = farmerRetailerOrderItemRepository;
            this.retailerOrderQueries = retailerOrderQueries;
            this.retailerOrderRepository = retailerOrderRepository;
            this.productQueries = productQueries;
            this.uoMQueries = uoMQueries;
        }
        public override async Task<int> HandleCommand(ProcessCommand request, CancellationToken cancellationToken)
        {
            var rs = 0;

            //Get statistic for this logic
            var products = await productQueries.Gets();
            var uoms = await uoMQueries.Gets();

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        farmerBuyingCalendarRepository.JoinTransaction(conn, trans);
                        farmerBuyingCalendarQueries.JoinTransaction(conn, trans);
                        farmerRetailerOrderItemRepository.JoinTransaction(conn, trans);
                        retailerOrderQueries.JoinTransaction(conn, trans);
                        retailerOrderRepository.JoinTransaction(conn, trans);

                        var order = await retailerOrderQueries.Get(request.Processing.OrderId);

                        if(order == null)
                        {
                            throw new BusinessException("Order.Retailer.Order.NotExisted");
                        }

                        order.StatusId = (int)RetailerOrderStatuses.FarmerOrdered;
                        order = UpdateBuild(order, request.LoginSession);
                        if ((await retailerOrderRepository.Update(order)) != 0)
                        {
                            return rs = -1;
                        }

                        foreach (var item in request.Processing.Items)
                        {
                            if(item.Plannings == null)
                            {
                                continue;
                            }

                            var orderItem = order.Items.FirstOrDefault(i => i.Id == item.OrderItemId);
                            
                            if(orderItem == null)
                            {
                                throw new BusinessException("OrderItem.NotExisted");
                            }

                            // wrong logic
                            if (!products.Any(p => p.Id == orderItem.ProductId) || !uoms.Any(u => u.Id == orderItem.UoMId))
                            {
                                return rs = -1;
                            }

                            orderItem.StatusId = (int)RetailerOrderStatuses.FarmerOrdered;
                            if ((await retailerOrderRepository.UpdateItem(orderItem)) != 0)
                            {
                                return rs = -1;
                            }

                            foreach (var planning in item.Plannings)
                            {
                                // Get farmer planning and check it existing
                                var checkingOrder = (await farmerBuyingCalendarQueries.Gets($"o.buying_date = '{order.BuyingDate.Date.ToString("yyyyMMdd")}' AND o.farmer_id = {planning.FarmerId} AND o.is_ordered = 0")).FirstOrDefault();
                                if (checkingOrder == null)
                                {
                                    //No existed

                                    //Create buying calendar
                                    var planningCode = await farmerBuyingCalendarQueries.GenarateCode();
                                    var addOrder = CreateBuild(new UI.Models.FarmerBuyingCalendar() {
                                        IsExpired = order.BuyingDate <= DateTime.Now ? true : false,
                                        IsOrdered = false,
                                        Code = planningCode,
                                        Name = planningCode,
                                        BuyingDate = order.BuyingDate,
                                        FarmerId = planning.FarmerId
                                    }, request.LoginSession);
                                    var planningId = await farmerBuyingCalendarRepository.Add(addOrder);

                                    //Create item
                                    var planningItem = new FarmerBuyingCalendarItem()
                                    {
                                        ProductId = orderItem.ProductId,
                                        UoMId = orderItem.UoMId,
                                        Quantity = planning.Quantity,
                                        AdapQuantity = planning.Quantity,
                                        FarmerBuyingCalendarId = planningId
                                    };
                                    var planningItemId = await farmerBuyingCalendarRepository.AddItem(planningItem);

                                    //Add mapping
                                    await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                    {
                                        IsPlanning = true,
                                        FarmerId = planning.FarmerId,
                                        FarmerOrderId = planningId,
                                        FarmerOrderItemId = planningItemId,
                                        RetailerId = order.RetailerId,
                                        RetailerOrderId = order.Id,
                                        RetailerOrderItemId = orderItem.Id,
                                        ProductId = orderItem.ProductId,
                                        Quantity = planning.Quantity,
                                        UoMId = orderItem.UoMId
                                    });
                                }
                                else
                                {
                                    //Existed 

                                    // Update information for Order
                                    checkingOrder = UpdateBuild(checkingOrder, request.LoginSession);
                                    if (await farmerBuyingCalendarRepository.Update(checkingOrder) != 0)
                                    {
                                        return rs = -1;
                                    }

                                    //Get the item and update quantity for item
                                    var checkingOrderItem = checkingOrder.Items.FirstOrDefault(i => i.ProductId == orderItem.ProductId && i.UoMId == orderItem.UoMId);
                                    if (checkingOrderItem == null)
                                    {

                                        //Create item
                                        var planningItem = new FarmerBuyingCalendarItem()
                                        {
                                            ProductId = orderItem.ProductId,
                                            UoMId = orderItem.UoMId,
                                            Quantity = planning.Quantity,
                                            AdapQuantity = planning.Quantity,
                                            FarmerBuyingCalendarId = checkingOrder.Id
                                        };
                                        var planningItemId = await farmerBuyingCalendarRepository.AddItem(planningItem);

                                        //Add mapping
                                        await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                        {
                                            IsPlanning = true,
                                            FarmerId = planning.FarmerId,
                                            FarmerOrderId = checkingOrder.Id,
                                            FarmerOrderItemId = planningItemId,
                                            RetailerId = order.RetailerId,
                                            RetailerOrderId = order.Id,
                                            RetailerOrderItemId = orderItem.Id,
                                            ProductId = orderItem.ProductId,
                                            Quantity = planning.Quantity,
                                            UoMId = orderItem.UoMId
                                        });
                                    }
                                    else
                                    {
                                        checkingOrderItem.Quantity += planning.Quantity;
                                        if (await farmerBuyingCalendarRepository.UpdateItem(checkingOrderItem) != 0)
                                        {
                                            return rs = -1;
                                        }

                                        //Add mapping
                                        await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                        {
                                            IsPlanning = true,
                                            FarmerId = planning.FarmerId,
                                            FarmerOrderId = checkingOrder.Id,
                                            FarmerOrderItemId = checkingOrderItem.Id,
                                            RetailerId = order.RetailerId,
                                            RetailerOrderId = order.Id,
                                            RetailerOrderItemId = orderItem.Id,
                                            ProductId = orderItem.ProductId,
                                            Quantity = planning.Quantity,
                                            UoMId = orderItem.UoMId
                                        });
                                    }
                                }
                            }
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
