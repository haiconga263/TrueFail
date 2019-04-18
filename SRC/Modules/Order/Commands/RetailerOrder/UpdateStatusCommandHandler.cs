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

namespace Order.Commands.RetailerOrder
{
    public class UpdateStatusCommandHandler : BaseCommandHandler<UpdateStatusCommand, int>
    {
        private readonly IRetailerOrderRepository retailerOrderRepository = null;
        private readonly IRetailerOrderQueries retailerOrderQueries = null;
        public UpdateStatusCommandHandler(IRetailerOrderRepository retailerOrderRepository,
                                    IRetailerOrderQueries retailerOrderQueries)
        {
            this.retailerOrderRepository = retailerOrderRepository;
            this.retailerOrderQueries = retailerOrderQueries;
        }
        public override async Task<int> HandleCommand(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderId == 0)
            {
                throw new BusinessException("Order.NotExisted");
            }

            if(!Enum.IsDefined(typeof(RetailerOrderStatuses), request.StatusId))
            {
                throw new BusinessException("Order.NotExistedStatus");
            }

            var order = await retailerOrderQueries.Get(request.OrderId);


            if (order != null)
            {
                if(request.StatusId == (int)RetailerOrderStatuses.Canceled && order.StatusId != (int)RetailerOrderStatuses.Ordered)
                {

                    //Cancel the order that is comfirmed.
                    throw new BusinessException("Order.CantCanceled");
                }
                else if(order.StatusId != request.StatusId - 1)
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
                            foreach (var item in order.Items)
                            {
                                item.StatusId = request.StatusId;
                                await retailerOrderRepository.UpdateItem(item);
                            }

                            order = UpdateBuild(order, request.LoginSession);
                            order.StatusId = request.StatusId;
                            await retailerOrderRepository.Update(order);

                            await retailerOrderRepository.AddAudit(new UI.Models.RetailerOrderAudit()
                            {
                                RetailerId = order.RetailerId,
                                RetailerOrderId = order.Id,
                                RetailerOrderItemId = null,
                                StatusId = request.StatusId,
                                CreatedBy = request.LoginSession.Id,
                                CreatedDate = DateTime.Now,
                                Note = string.Empty
                            });

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

            throw new BusinessException("Order.NotExisted");
        }
    }
}
