using DAL;
using MDM.UI.Products.Interfaces;
using MDM.UI.UoMs.Interfaces;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Order.Commands.FarmerBuyingCalendar
{
    public class ProcessInRetailerOrderCommandHandler : BaseCommandHandler<ProcessInRetailerOrderCommand, int>
    {
        private readonly IFarmerBuyingCalendarRepository farmerBuyingCalendarRepository = null;
        private readonly IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries = null;
        private readonly IFarmerRetailerOrderItemRepository farmerRetailerOrderItemRepository = null;
        private readonly IProductQueries productQueries = null;
        private readonly IUoMQueries uoMQueries = null;
        public ProcessInRetailerOrderCommandHandler(IFarmerBuyingCalendarRepository farmerBuyingCalendarRepository, 
                                     IFarmerBuyingCalendarQueries farmerBuyingCalendarQueries,
                                     IFarmerRetailerOrderItemRepository farmerRetailerOrderItemRepository,
                                     IProductQueries productQueries,
                                     IUoMQueries uoMQueries)
        {
            this.farmerBuyingCalendarRepository = farmerBuyingCalendarRepository;
            this.farmerBuyingCalendarQueries = farmerBuyingCalendarQueries;
            this.farmerRetailerOrderItemRepository = farmerRetailerOrderItemRepository;
            this.productQueries = productQueries;
            this.uoMQueries = uoMQueries;
        }
        public override async Task<int> HandleCommand(ProcessInRetailerOrderCommand request, CancellationToken cancellationToken)
        {
            var rs = 0;

            //Get statistic for this logic
            var products = await productQueries.Gets();
            var uoms = await uoMQueries.Gets();

            using (var conn = DALHelper.GetConnection())
            {
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        farmerBuyingCalendarRepository.JoinTransaction(conn, trans);
                        farmerBuyingCalendarQueries.JoinTransaction(conn, trans);
                        farmerRetailerOrderItemRepository.JoinTransaction(conn, trans);

                        foreach (var order in request.BuyingCalendars)
                        {
                            // wrong logic
                            if (order.FarmerId == 0)
                            {
                                return rs = -1;
                            }

                            // Add new
                            if (order.Id == 0)
                            {
                                foreach (var item in order.Items)
                                {
                                    // wrong logic
                                    if (!products.Any(p => p.Id == item.ProductId) || !uoms.Any(u => u.Id == item.UoMId))
                                    {
                                        return rs = -1;
                                    }

                                    // Get farmer order and check it existing
                                    var checkingOrder = (await farmerBuyingCalendarQueries.Gets($"i.product_id = {item.Id} AND i.uom_id == {item.UoMId} AND o.buying_date = '{order.BuyingDate.Date}' AND o.farmer_id = {order.FarmerId} AND o.is_ordered = 0")).FirstOrDefault();
                                    if (checkingOrder == null)
                                    {
                                        //No existed

                                        //Create buying calendar
                                        order.IsExpired = order.BuyingDate <= DateTime.Now ? true : false;
                                        order.IsOrdered = false;
                                        var addOrder = CreateBuild(order, request.LoginSession);
                                        var orderId = await farmerBuyingCalendarRepository.Add(addOrder);

                                        //Create item
                                        item.FarmerBuyingCalendarId = orderId;
                                        var orderItemId = await farmerBuyingCalendarRepository.AddItem(item);

                                        //Add mapping
                                        foreach (var FRitem in item.FarmerRetailerOrderItems)
                                        {
                                            await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                            {
                                                FarmerId = order.FarmerId,
                                                FarmerOrderId = orderId,
                                                FarmerOrderItemId = orderItemId,
                                                RetailerId = FRitem.RetailerId,
                                                RetailerOrderId = FRitem.RetailerOrderId,
                                                RetailerOrderItemId = FRitem.RetailerOrderItemId,
                                                ProductId = item.ProductId,
                                                Quantity = FRitem.Quantity,
                                                UoMId = item.UoMId
                                            });
                                        }
                                    }
                                    else
                                    {
                                        //Existed 

                                        // Update information for Order
                                        var updateOrder = UpdateBuild(order, request.LoginSession);
                                        if(await farmerBuyingCalendarRepository.Update(updateOrder) != 0)
                                        {
                                            return rs = -1;
                                        }

                                        //Get the item and update quantity for item
                                        var checkingOrderItem = updateOrder.Items.First(i => i.ProductId == item.ProductId && item.UoMId == item.UoMId);
                                        checkingOrderItem.Quantity += item.Quantity;
                                        if (await farmerBuyingCalendarRepository.UpdateItem(item) != 0)
                                        {
                                            return rs = -1;
                                        }

                                        // Add mapping
                                        foreach (var FRitem in item.FarmerRetailerOrderItems)
                                        {
                                            await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                            {
                                                FarmerId = order.FarmerId,
                                                FarmerOrderId = updateOrder.Id,
                                                FarmerOrderItemId = checkingOrderItem.Id,
                                                RetailerId = FRitem.RetailerId,
                                                RetailerOrderId = FRitem.RetailerOrderId,
                                                RetailerOrderItemId = FRitem.RetailerOrderItemId,
                                                ProductId = item.ProductId,
                                                Quantity = FRitem.Quantity,
                                                UoMId = item.UoMId
                                            });
                                        }
                                    }
                                }
                            }
                            else //Modify
                            {
                                // Get farmer order and check it existing
                                var checkingOrder = await farmerBuyingCalendarQueries.Get(order.Id);
                                if(checkingOrder == null)
                                {
                                    return rs = -1;
                                }

                                checkingOrder = UpdateBuild(checkingOrder, request.LoginSession);
                                if (await farmerBuyingCalendarRepository.Update(checkingOrder) != 0)
                                {
                                    return rs = -1;
                                }

                                foreach (var item in order.Items)
                                {
                                    // wrong logic
                                    if (!products.Any(p => p.Id == item.ProductId) || !uoms.Any(u => u.Id == item.UoMId))
                                    {
                                        return rs = -1;
                                    }

                                    // New item
                                    if (item.Id == 0)
                                    {
                                        var checkingOrderItem = checkingOrder.Items.FirstOrDefault(i => i.ProductId == item.ProductId && i.UoMId == item.UoMId);

                                        if (checkingOrderItem == null)
                                        {
                                            //Create item
                                            item.FarmerBuyingCalendarId = checkingOrder.Id;
                                            var orderItemId = await farmerBuyingCalendarRepository.AddItem(item);
                                            checkingOrder.Items.Add(item); //Add for next checking

                                            //Add mapping
                                            foreach (var FRitem in item.FarmerRetailerOrderItems)
                                            {
                                                await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                                {
                                                    FarmerId = order.FarmerId,
                                                    FarmerOrderId = checkingOrder.Id,
                                                    FarmerOrderItemId = orderItemId,
                                                    RetailerId = FRitem.RetailerId,
                                                    RetailerOrderId = FRitem.RetailerOrderId,
                                                    RetailerOrderItemId = FRitem.RetailerOrderItemId,
                                                    ProductId = item.ProductId,
                                                    Quantity = FRitem.Quantity,
                                                    UoMId = item.UoMId
                                                });
                                            }
                                        }
                                        else //New item but product and UoM is same as another item in this order
                                        {
                                            checkingOrderItem.Quantity += item.Quantity;
                                            await farmerBuyingCalendarRepository.UpdateItem(item);

                                            // Add mapping
                                            foreach (var FRitem in item.FarmerRetailerOrderItems)
                                            {
                                                await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                                {
                                                    FarmerId = order.FarmerId,
                                                    FarmerOrderId = checkingOrderItem.Id,
                                                    FarmerOrderItemId = checkingOrderItem.Id,
                                                    RetailerId = FRitem.RetailerId,
                                                    RetailerOrderId = FRitem.RetailerOrderId,
                                                    RetailerOrderItemId = FRitem.RetailerOrderItemId,
                                                    ProductId = item.ProductId,
                                                    Quantity = FRitem.Quantity,
                                                    UoMId = item.UoMId
                                                });
                                            }
                                        }
                                    }
                                    else //Modify
                                    {
                                        var checkingOrderItem = checkingOrder.Items.FirstOrDefault(i => i.ProductId == item.ProductId && i.UoMId == item.UoMId);

                                        if (checkingOrderItem == null)
                                        {
                                            return rs = -1;
                                        }

                                        //Update Item
                                        checkingOrderItem.Quantity = item.Quantity;
                                        if (await farmerBuyingCalendarRepository.UpdateItem(checkingOrderItem) != 0)
                                        {
                                            return rs = -1;
                                        }

                                        //Delete all mapping with this item
                                        await farmerRetailerOrderItemRepository.Delete($"`farmer_item_id` = {checkingOrderItem.Id}");

                                        //Add mapping again
                                        foreach (var FRitem in item.FarmerRetailerOrderItems)
                                        {
                                            await farmerRetailerOrderItemRepository.Add(new UI.Models.FarmerRetailerOrderItems()
                                            {
                                                FarmerId = order.FarmerId,
                                                FarmerOrderId = checkingOrderItem.Id,
                                                FarmerOrderItemId = checkingOrderItem.Id,
                                                RetailerId = FRitem.RetailerId,
                                                RetailerOrderId = FRitem.RetailerOrderId,
                                                RetailerOrderItemId = FRitem.RetailerOrderItemId,
                                                ProductId = item.ProductId,
                                                Quantity = FRitem.Quantity,
                                                UoMId = item.UoMId
                                            });
                                        }
                                    }
                                }
                            }
                        }

                        trans.Commit();
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
