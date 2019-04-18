using Common;
using Common.Exceptions;
using Common.Models;
using DAL;
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
    public class UpdateItemStatusCommandHandler : BaseCommandHandler<UpdateItemStatusCommand, int>
    {
        private readonly IRetailerOrderRepository retailerOrderRepository = null;
        private readonly IRetailerOrderQueries retailerOrderQueries = null;
        public UpdateItemStatusCommandHandler(IRetailerOrderRepository retailerOrderRepository,
                                    IRetailerOrderQueries retailerOrderQueries)
        {
            this.retailerOrderRepository = retailerOrderRepository;
            this.retailerOrderQueries = retailerOrderQueries;
        }
        public override async Task<int> HandleCommand(UpdateItemStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderId == 0 && request.OrderItemId == 0)
            {
                throw new BusinessException("OrderItem.NotExisted");
            }

            if (!Enum.IsDefined(typeof(RetailerOrderStatuses), request.StatusId))
            {
                throw new BusinessException("Order.NotExistedStatus");
            }

            var order = await retailerOrderQueries.Get(request.OrderId);

            if (order != null)
            {
                if (request.StatusId == (int)RetailerOrderStatuses.Canceled && order.StatusId != (int)RetailerOrderStatuses.Ordered)
                {

                    //Cancel the order that is comfirmed.
                    throw new BusinessException("Order.CantCanceled");
                }
                else if (order.StatusId <= request.StatusId)
                {
                    //wrong step; dump data
                    throw new BusinessException("Order.WrongStep");
                }

                var rs = -1;

                using (var conn = DALHelper.GetConnection())
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            retailerOrderRepository.JoinTransaction(conn, trans);
                            var item = order.Items.FirstOrDefault(i => i.Id == request.OrderItemId);
                            if(item == null)
                            {
                                throw new BusinessException("OrderItem.NotExisted");
                            }

                            item.StatusId = request.StatusId;

                            await retailerOrderRepository.UpdateItem(item);

                            await retailerOrderRepository.AddAudit(new UI.Models.RetailerOrderAudit()
                            {
                                RetailerId = order.RetailerId,
                                RetailerOrderId = order.Id,
                                RetailerOrderItemId = item.Id,
                                StatusId = request.StatusId,
                                CreatedBy = request.LoginSession.Id,
                                CreatedDate = DateTime.Now,
                                Note = string.Empty
                            });

                            order.StatusId = request.StatusId;
                            order = UpdateBuild(order, request.LoginSession);
                            await retailerOrderRepository.Update(order);
                            return rs = 0;
                        }
                        finally
                        {
                            if (rs == 0)
                            {
                                trans.Commit();
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
            }

            throw new BusinessException("OrderItem.NotExisted");
        }

        public override async Task<int> HandleCommandTransaction(UpdateItemStatusCommand request, CachingCommandTransactionModel trans, CancellationToken cancellationToken)
        {
            if (request.OrderId == 0 && request.OrderItemId == 0)
            {
                throw new BusinessException("OrderItem.NotExisted");
            }

            if (!Enum.IsDefined(typeof(RetailerOrderStatuses), request.StatusId))
            {
                throw new BusinessException("Order.NotExistedStatus");
            }

            retailerOrderQueries.JoinTransaction(trans.Connection, trans.Transaction);

            var order = await retailerOrderQueries.Get(request.OrderId);

            if (order != null)
            {
                if (request.StatusId == (int)RetailerOrderStatuses.Canceled && order.StatusId != (int)RetailerOrderStatuses.Ordered)
                {

                    //Cancel the order that is comfirmed.
                    throw new BusinessException("Order.CantCanceled");
                }
                else if (order.StatusId <= request.StatusId)
                {
                    //wrong step; dump data
                    throw new BusinessException("Order.WrongStep");
                }

                retailerOrderRepository.JoinTransaction(trans.Connection, trans.Transaction);
                var item = order.Items.FirstOrDefault(i => i.Id == request.OrderItemId);
                if (item == null)
                {
                    throw new BusinessException("OrderItem.NotExisted");
                }

                item.StatusId = request.StatusId;

                await retailerOrderRepository.UpdateItem(item);

                await retailerOrderRepository.AddAudit(new UI.Models.RetailerOrderAudit()
                {
                    RetailerId = order.RetailerId,
                    RetailerOrderId = order.Id,
                    RetailerOrderItemId = item.Id,
                    StatusId = request.StatusId,
                    CreatedBy = request.LoginSession.Id,
                    CreatedDate = DateTime.Now,
                    Note = string.Empty
                });

                order.StatusId = request.StatusId;
                order = UpdateBuild(order, request.LoginSession);
                return await retailerOrderRepository.Update(order);
            }

            throw new BusinessException("OrderItem.NotExisted");
        }
    }
}
