using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Products.Interfaces;
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
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IRetailerOrderRepository retailerOrderRepository = null;
        private readonly IRetailerOrderQueries retailerOrderQueries = null;
        private readonly IProductQueries productQueries = null;
        public UpdateCommandHandler(IRetailerOrderRepository retailerOrderRepository, 
                                    IRetailerOrderQueries retailerOrderQueries,
                                    IProductQueries productQueries)
        {
            this.retailerOrderRepository = retailerOrderRepository;
            this.retailerOrderQueries = retailerOrderQueries;
            this.productQueries = productQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Order == null || request.Order.Id == 0 
               || request.Order.Items == null || request.Order.Items.Count == 0)
            {
                throw new BusinessException("Order.Retailer.Order.NotExisted");
            }

            var retailerId = -1;
            if (request.LoginSession.Roles.FirstOrDefault(r => r == "Administrator") == null)
            {
                var retailer = await WebHelper.HttpGet<Retailer>(GlobalConfiguration.APIGateWayURI, $"{AppUrl.GetRetailerByUser}?userId={request.LoginSession.Id}", request.LoginSession.AccessToken);
                if (retailer == null)
                {
                    throw new BusinessException("Retailer.NotExisted");
                }
                retailerId = retailer.Id;
            }

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        retailerOrderRepository.JoinTransaction(conn, trans);
                        retailerOrderQueries.JoinTransaction(conn, trans);
                        productQueries.JoinTransaction(conn, trans);
                        var order = await retailerOrderQueries.Get(request.Order.Id);

                        if(order == null)
                        {
                            throw new BusinessException("Order.Retailer.Order.NotExisted");
                        }

                        if(retailerId != -1 && retailerId != order.RetailerId)
                        {
                            throw new NotPermissionException();
                        }

                        if(order.CreatedDate.AddSeconds(GlobalConfiguration.TimeoutOfRetailerOrder) < DateTime.Now)
                        {
                            throw new BusinessException("Order.Retailer.Order.TimeOut");
                        }

                        decimal totalAmount = 0;
                        await retailerOrderRepository.DeleteItems(request.Order.Id);
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

                        request.Order = UpdateBuild(request.Order, request.LoginSession);
                        request.Order.RetailerId = order.RetailerId;
                        request.Order.StatusId = (int)RetailerOrderStatuses.Ordered;
                        request.Order.RetailerBuyingCalendarId = order.RetailerBuyingCalendarId;
                        request.Order.TotalAmount = totalAmount;
                        request.Order.CreatedBy = order.CreatedBy;
                        request.Order.CreatedDate = order.CreatedDate;
                        if (await retailerOrderRepository.Update(request.Order) != 0)
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
