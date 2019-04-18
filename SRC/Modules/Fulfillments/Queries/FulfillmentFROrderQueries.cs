using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using Fulfillments.UI.Interfaces;
using Fulfillments.UI.Models;
using Fulfillments.UI.ViewModels;
using MDM.UI.Products.Models;
using MDM.UI.Retailers.Models;
using MDM.UI.UoMs.Models;
using Order.UI.Models;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fulfillments.Queries
{
    public class FulfillmentFROrderQueries : BaseQueries, IFulfillmentFROrderQueries
    {
        public async Task<IEnumerable<FulfillmentRetailerOrderViewModel>> GetRetailerOrderForPack(string retailerOrderId)
        {
            List<FulfillmentRetailerOrderViewModel> result = new List<FulfillmentRetailerOrderViewModel>();
            string cmd = $@"SELECT * FROM `retailer_order` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_order_item` i ON i.retailer_order_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.id = {retailerOrderId} AND o.status_id = 5 AND o.is_deleted = 0";

            DbConnection = DbConnection ?? DALHelper.GetConnection();
            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            rd.Read<FulfillmentRetailerOrder, Retailer, FulfillmentRetailerOrderItem, Product, UoM, FulfillmentRetailerOrderViewModel>(
                (orderRs, rRs, itemRs, pRs, uRs) =>
                {
                    var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                    if (order == null)
                    {
                        order = CommonHelper.Mapper<FulfillmentRetailerOrder, FulfillmentRetailerOrderViewModel>(orderRs);
                        result.Add(order);
                    }

                    if (order.Retailer == null)
                    {
                        order.Retailer = rRs;
                    }

                    var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                    if (item == null)
                    {
                        item = CommonHelper.Mapper<FulfillmentRetailerOrderItem, FulfillmentRetailerOrderItemViewModel>(itemRs);
                        order.Items.Add(item);
                    }


                    item.Product = pRs;
                    item.UoM = uRs;

                    return order;
                }
            );

            return result;
        }

        public async Task<IEnumerable<FulfillmentRetailerOrderViewModel>> GetRetailerOrderForPackByFulId(string fulfillmentId)
        {
            List<FulfillmentRetailerOrderViewModel> result = new List<FulfillmentRetailerOrderViewModel>();
            string cmd = $@"SELECT * FROM `retailer_order` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_order_item` i ON i.retailer_order_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.`fulfillment_id_to` = {fulfillmentId} AND o.status_id = 5 AND o.is_deleted = 0";

            DbConnection = DbConnection ?? DALHelper.GetConnection();
            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            rd.Read<FulfillmentRetailerOrder, Retailer, FulfillmentRetailerOrderItem, Product, UoM, FulfillmentRetailerOrderViewModel>(
                (orderRs, rRs, itemRs, pRs, uRs) =>
                {
                    var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                    if (order == null)
                    {
                        order = CommonHelper.Mapper<FulfillmentRetailerOrder, FulfillmentRetailerOrderViewModel>(orderRs);
                        result.Add(order);
                    }

                    if (order.Retailer == null)
                    {
                        order.Retailer = rRs;
                    }

                    var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                    if (item == null)
                    {
                        item = CommonHelper.Mapper<FulfillmentRetailerOrderItem, FulfillmentRetailerOrderItemViewModel>(itemRs);
                        order.Items.Add(item);
                    }


                    item.Product = pRs;
                    item.UoM = uRs;

                    return order;
                }
            );

            return result;
        }

        public async Task<IEnumerable<FulfillmentTeam>> GetTeam()
        {
            List<FulfillmentTeam> result = new List<FulfillmentTeam>();
            string cmd = $@"SELECT * FROM `fulfillment_team`                             
                            WHERE is_used = 1 AND is_deleted = 0";
            return await DALHelper.Query<FulfillmentTeam>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
        
    }
}
