using Common;
using DAL;
using MDM.UI.Products.Interfaces;
using MediatR;
using Order.Commands.RetailerOrder;
using Order.UI;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web;
using Web.Controllers;
using Web.Helpers;

namespace Order.Commands.FarmerOrder
{
    public class UpdateInCollectorCommandHandler : BaseCommandHandler<UpdateInCollectorCommand, int>
    {
        private readonly IFarmerOrderRepository farmerOrderRepository = null;
        private readonly IFarmerOrderQueries farmerOrderQueries = null;
        private readonly IFarmerRetailerOrderItemQueries farmerRetailerOrderItemQueries = null;
        private readonly IMediator mediator = null;
        public UpdateInCollectorCommandHandler(IFarmerOrderRepository farmerOrderRepository, 
                                    IFarmerOrderQueries farmerOrderQueries,
                                    IFarmerRetailerOrderItemQueries farmerRetailerOrderItemQueries,
                                    IMediator mediator)
        {
            this.farmerOrderRepository = farmerOrderRepository;
            this.farmerOrderQueries = farmerOrderQueries;
            this.farmerRetailerOrderItemQueries = farmerRetailerOrderItemQueries;
            this.mediator = mediator;
        }
        public override async Task<int> HandleCommand(UpdateInCollectorCommand request, CancellationToken cancellationToken)
        {
            if(request.Order == null || request.Order.Id == 0 
               || request.Order.Items == null || request.Order.Items.Count == 0)
            {
                return -1;
            }

            var rs = 0;

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    string tranId = Guid.NewGuid().ToString().Replace("-", "");
                    try
                    {

                        farmerOrderRepository.JoinTransaction(conn, trans);
                        farmerOrderQueries.JoinTransaction(conn, trans);
                        farmerRetailerOrderItemQueries.JoinTransaction(conn, trans);

                        var order = await farmerOrderQueries.Get(request.Order.Id);

                        if(order.FarmerId != request.Order.FarmerId)
                        {
                            return rs = -1; //fake api
                        }

                        if (!Enum.IsDefined(typeof(FarmerOrderStatuses), request.Order.StatusId))
                        {
                            return rs = -1; //wrong status
                        }

                        var status = (FarmerOrderStatuses)request.Order.StatusId;
                        if(status != FarmerOrderStatuses.Canceled && status != FarmerOrderStatuses.Completed)
                        {
                            return rs = -1;
                        }

                        var frOrders = await farmerRetailerOrderItemQueries.GetByBC(order.Id);

                        decimal totalAmount = 0;
                        foreach (var item in request.Order.Items)
                        {
                            var orderItem = order.Items.First(i => i.Id == item.Id);
                            orderItem.StatusId = (int)status;
                            orderItem.DeliveriedQuantity = status == FarmerOrderStatuses.Canceled ? 0 : item.DeliveriedQuantity;
                            await farmerOrderRepository.UpdateItem(item);

                            totalAmount += orderItem.Price * item.DeliveriedQuantity;

                            if (status == FarmerOrderStatuses.Completed)
                            {
                                await WebHelper.HttpPost<object>(GlobalConfiguration.APIGateWayURI, AppUrl.SetCollectionInventory, new {
                                    Direction = 1,
                                    request.Order.CollectionId,
                                    TraceCode = GetMockTraceCode(),
                                    item.ProductId,
                                    item.UoMId,
                                    Quantity = item.DeliveriedQuantity,
                                    CommandStyle = (int)CommandStyles.Transaction,
                                    CommandId = tranId
                                }, request.LoginSession.AccessToken);

                                var mappingItem = frOrders.FirstOrDefault(i => i.IsPlanning == false && i.FarmerOrderItemId == item.Id);
                                if(mappingItem != null)
                                {
                                    await mediator.Send(new UpdateItemStatusCommand(
                                            mappingItem.RetailerOrderId,
                                            mappingItem.RetailerOrderItemId,
                                            (int)RetailerOrderStatuses.InConllections
                                        )
                                    {
                                        CommandId = tranId,
                                        LoginSession = request.LoginSession,
                                        CommandStyle = (int)CommandStyles.Transaction
                                    }
                                    );
                                }
                            }
                        }

                        request.Order = UpdateBuild(request.Order, request.LoginSession);
                        request.Order.StatusId = (int)status;
                        request.Order.FarmerBuyingCalendarId = order.FarmerBuyingCalendarId;
                        request.Order.TotalAmount = status == FarmerOrderStatuses.Canceled ? 0 : totalAmount;
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
                        {
                            // commit
                            try
                            {
                                await WebHelper.HttpPost<object>(GlobalConfiguration.APIGateWayURI, AppUrl.SetCollectionInventory, new
                                {
                                    CommandStyle = (int)CommandStyles.CommitTransaction,
                                    CommandId = tranId
                                }, request.LoginSession.AccessToken);

                                await mediator.Send(new UpdateItemStatusCommand(0, 0, 0)
                                {
                                    CommandId = tranId,
                                    LoginSession = request.LoginSession,
                                    CommandStyle = (int)CommandStyles.CommitTransaction
                                });

                                trans.Commit();
                            }
                            catch (Exception ex) {

                                trans.Rollback();
                                throw ex;
                            }
                        }
                        else
                        {
                            try
                            {
                                // rollback
                                await WebHelper.HttpPost<object>(GlobalConfiguration.APIGateWayURI, AppUrl.SetCollectionInventory, new
                                {
                                    CommandStyle = (int)CommandStyles.RollbackTransaction,
                                    CommandId = tranId
                                }, request.LoginSession.AccessToken);
                            }
                            catch { }
                            try
                            {
                                // rollback
                                await mediator.Send(new UpdateItemStatusCommand(0, 0, 0)
                                {
                                    CommandId = tranId,
                                    LoginSession = request.LoginSession,
                                    CommandStyle = (int)CommandStyles.RollbackTransaction
                                });
                            }
                            catch { }
                            try
                            {
                                trans.Rollback();
                            }
                            catch {  }
                        }
                    }
                }
            }

           return rs;
        }

        public string GetMockTraceCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 17);
        }
    }
}
