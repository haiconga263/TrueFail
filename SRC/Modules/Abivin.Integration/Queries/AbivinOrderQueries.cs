using Abivin.Integration.UI.Interfaces;
using Abivin.Integration.UI.ViewModels;
using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
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

namespace Abivin.Integration.Queries
{
    public class AbivinOrderQueries : BaseQueries, IAbivinOrderQueries
    {
        public async Task<IEnumerable<OrderViewModel>> Get(string code)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            string cmd = $@"SELECT * FROM `retailer_order` o
                            LEFT JOIN `retailer_location` l ON o.ship_to = l.id AND l.is_used = 1 AND l.is_deleted = 0
                            LEFT JOIN `retailer_order_item` i ON i.retailer_order_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            WHERE o.is_deleted = 0 and o.code = '{code}'";

            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerOrder, RetailerLocation, RetailerOrderItem, Product, OrderViewModel>(
                    (orderRs, lRs, itemRs, pRs) =>
                    {
                        var order = new OrderViewModel()
                        {
                            OrderDate = orderRs.BuyingDate.ToString("MM/dd/yyyy"),
                            OrderCode = orderRs.Code,
                            OrderType = "SALES", //hardcode
                            PartnerCode = lRs.GLN,
                            ProductCode = $"{pRs.Code}_{itemRs.UoMId}",
                            NumberOfCases = 0, //hardcode
                            NumberOfItems = itemRs.OrderedQuantity,
                            TotalPrice = itemRs.Price * itemRs.OrderedQuantity,
                            CustomerDiscount = 0, //hardcode
                            SaleDiscount = 0, //hardcode
                            PromotionDiscount = 0, //hardcode
                            IMVDDiscount = 0, //hardcode
                            PickupOrder = 0, //hardcode ; Don hang gui
                            ServiceTime = 0, //hardcode
                            Splitted = false, //hardcode
                            TimeWindow = "0", //hardcode
                            LotNumber = string.Empty, //hardcode
                            ExpiredDate = orderRs.BuyingDate.AddDays(1.0).ToString("MM/dd/yyyy")
                        };
                        result.Add(order);

                        return order;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<RetailerOrder, RetailerLocation, RetailerOrderItem, Product, OrderViewModel>(
                        (orderRs, lRs, itemRs, pRs) =>
                        {
                            var order = new OrderViewModel()
                            {
                                OrderDate = orderRs.BuyingDate.ToString("MM/dd/yyyy"),
                                OrderCode = orderRs.Code,
                                OrderType = "SALES", //hardcode
                                PartnerCode = lRs.GLN,
                                ProductCode = $"{pRs.Code}_{itemRs.UoMId}",
                                NumberOfCases = 0, //hardcode
                                NumberOfItems = itemRs.OrderedQuantity,
                                TotalPrice = itemRs.Price * itemRs.OrderedQuantity,
                                CustomerDiscount = 0, //hardcode
                                SaleDiscount = 0, //hardcode
                                PromotionDiscount = 0, //hardcode
                                IMVDDiscount = 0, //hardcode
                                PickupOrder = 0, //hardcode ; Don hang gui
                                ServiceTime = 0, //hardcode
                                Splitted = false, //hardcode
                                TimeWindow = "0", //hardcode
                                LotNumber = string.Empty, //hardcode
                                ExpiredDate = orderRs.BuyingDate.AddDays(1.0).ToString("MM/dd/yyyy")
                            };
                            result.Add(order);

                            return order;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<RetailerOrderViewModel> GetByCode(string code)
        {
            RetailerOrderViewModel result = null;
            string cmd = $@"SELECT * FROM `retailer_order` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_order_item` i ON i.retailer_order_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.`code` = '{code}' AND o.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerOrder, Retailer, RetailerOrderItem, Product, UoM, RetailerOrderViewModel>(
                    (orderRs, rRs, itemRs, pRs, uRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<RetailerOrder, RetailerOrderViewModel>(orderRs);
                        }

                        if (result.Retailer == null)
                        {
                            result.Retailer = rRs;
                        }

                        var item = result.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<RetailerOrderItem, RetailerOrderItemViewModel>(itemRs);
                            result.Items.Add(item);
                        }

                        item.Product = pRs;
                        item.UoM = uRs;

                        return result;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<RetailerOrder, Retailer, RetailerOrderItem, Product, UoM, RetailerOrderViewModel>(
                        (orderRs, rRs, itemRs, pRs, uRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<RetailerOrder, RetailerOrderViewModel>(orderRs);
                            }

                            if (result.Retailer == null)
                            {
                                result.Retailer = rRs;
                            }

                            var item = result.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<RetailerOrderItem, RetailerOrderItemViewModel>(itemRs);
                                result.Items.Add(item);
                            }

                            item.Product = pRs;
                            item.UoM = uRs;

                            return result;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<OrderViewModel>> Gets(string condition = "")
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            string cmd = $@"SELECT * FROM `retailer_order` o
                            LEFT JOIN `retailer_location` l ON o.ship_to = l.id AND l.is_used = 1 AND l.is_deleted = 0
                            LEFT JOIN `retailer_order_item` i ON i.retailer_order_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            WHERE o.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += $" and {condition}";
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerOrder, RetailerLocation, RetailerOrderItem, Product, OrderViewModel>(
                    (orderRs, lRs, itemRs, pRs) =>
                    {
                        var order = new OrderViewModel()
                        {
                            OrderDate = orderRs.BuyingDate.ToString("MM/dd/yyyy"),
                            OrderCode = orderRs.Code,
                            OrderType = "SALES", //hardcode
                            PartnerCode = lRs.GLN,
                            ProductCode = $"{pRs.Code}_{itemRs.UoMId}",
                            NumberOfCases = 0, //hardcode
                            NumberOfItems = itemRs.OrderedQuantity,
                            TotalPrice = itemRs.Price * itemRs.OrderedQuantity,
                            CustomerDiscount = 0, //hardcode
                            SaleDiscount = 0, //hardcode
                            PromotionDiscount = 0, //hardcode
                            IMVDDiscount = 0, //hardcode
                            PickupOrder = 0, //hardcode ; Don hang gui
                            ServiceTime = 0, //hardcode
                            Splitted = false, //hardcode
                            TimeWindow = "0", //hardcode
                            LotNumber = string.Empty, //hardcode
                            ExpiredDate = orderRs.BuyingDate.AddDays(1.0).ToString("MM/dd/yyyy")
                        };
                        result.Add(order);

                        return order;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<RetailerOrder, RetailerLocation, RetailerOrderItem, Product, OrderViewModel>(
                        (orderRs, lRs, itemRs, pRs) =>
                        {
                            var order = new OrderViewModel()
                            {
                                OrderDate = orderRs.BuyingDate.ToString("MM/dd/yyyy"),
                                OrderCode = orderRs.Code,
                                OrderType = "SALES", //hardcode
                                PartnerCode = lRs.GLN,
                                ProductCode = $"{pRs.Code}_{itemRs.UoMId}",
                                NumberOfCases = 0, //hardcode
                                NumberOfItems = itemRs.OrderedQuantity,
                                TotalPrice = itemRs.Price * itemRs.OrderedQuantity,
                                CustomerDiscount = 0, //hardcode
                                SaleDiscount = 0, //hardcode
                                PromotionDiscount = 0, //hardcode
                                IMVDDiscount = 0, //hardcode
                                PickupOrder = 0, //hardcode ; Don hang gui
                                ServiceTime = 0, //hardcode
                                Splitted = false, //hardcode
                                TimeWindow = "0", //hardcode
                                LotNumber = string.Empty, //hardcode
                                ExpiredDate = orderRs.BuyingDate.AddDays(1.0).ToString("MM/dd/yyyy")
                            };
                            result.Add(order);

                            return order;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
