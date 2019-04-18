using Common;
using DAL;
using MDM.UI.Products.Interfaces;
using Order.UI;
using Order.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Order.Commands.FarmerOrder
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IFarmerOrderRepository farmerOrderRepository = null;
        private readonly IFarmerOrderQueries farmerOrderQueries = null;
        private readonly IProductQueries productQueries = null;
        public UpdateCommandHandler(IFarmerOrderRepository farmerOrderRepository, 
                                    IFarmerOrderQueries farmerOrderQueries,
                                    IProductQueries productQueries)
        {
            this.farmerOrderRepository = farmerOrderRepository;
            this.farmerOrderQueries = farmerOrderQueries;
            this.productQueries = productQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
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
                using (var trans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        farmerOrderRepository.JoinTransaction(conn, trans);
                        farmerOrderQueries.JoinTransaction(conn, trans);
                        productQueries.JoinTransaction(conn, trans);
                        var order = await farmerOrderQueries.Get(request.Order.Id);

                        if(order.FarmerId != request.Order.FarmerId)
                        {
                            return rs = -1; //fake api
                        }

                        #warning Hoang Uncompleted
                        //check business update farmer order here
                        bool isOk = false;
                        if(isOk)
                        {
                            return rs = -1; //Time out
                        }

                        decimal totalAmount = 0;
                        await farmerOrderRepository.DeleteItems(request.Order.Id);
                        foreach (var item in request.Order.Items)
                        {
                            var prod = (await productQueries.Get(item.ProductId)).FirstOrDefault(p => p.CurrentUoM == item.UoMId);
                            if (prod == null)
                            {
                                throw new Exception("Product doesn't existed");
                            }

                            item.Id = request.Order.Id;
                            item.StatusId = (int)FarmerOrderStatuses.BeginOrder;
                            item.DeliveriedQuantity = 0;
                            item.Price = prod.BuyingCurrentPrice;
                            await farmerOrderRepository.AddItem(item);

                            totalAmount += prod.BuyingCurrentPrice * item.OrderedQuantity;
                        }

                        request.Order = UpdateBuild(request.Order, request.LoginSession);
                        request.Order.StatusId = (int)FarmerOrderStatuses.BeginOrder;
                        request.Order.FarmerBuyingCalendarId = order.FarmerBuyingCalendarId;
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

           return rs;
        }
    }
}
