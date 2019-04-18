using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Products.Models;
using MDM.UI.Retailers.Models;
using MDM.UI.UoMs.Models;
using Order.UI.Interfaces;
using Order.UI.Models;
using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Queries
{
    public class RetailerBuyingCalendarQueries : BaseQueries, IRetailerBuyingCalendarQueries
    {
        private const string OrderCodeFormat = "P{y}{n}"; // O<year><number>
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `retailer_buying_calendar`");
            if (previousCode == null)
            {
                code = OrderCodeFormat.Replace("{y}", DateTime.Now.Year.ToString("0000")).Replace("{n}", 1.ToString("00000"));
            }
            else
            {
                if (DateTime.Now.Year.ToString("0000").Equals(previousCode.Substring(1, 4)))
                {
                    code = previousCode.Substring(0, 5) + (Int32.Parse(previousCode.Substring(5, 5)) + 1).ToString("00000");
                }
            }

            return code;
        }

        public async Task<RetailerBuyingCalendarViewModel> Get(long id)
        {
            RetailerBuyingCalendarViewModel result = null;
            string cmd = $@"SELECT * FROM `retailer_buying_calendar` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_buying_calendar_item` i ON i.retailer_buying_calendar_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.`id` = {id} AND o.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerBuyingCalendar, Retailer, RetailerBuyingCalendarItem, Product, UoM, RetailerBuyingCalendarViewModel>(
                    (orderRs, rRs, itemRs, pRs, uRs) =>
                    {
                        if(result == null)
                        {
                            result = CommonHelper.Mapper<RetailerBuyingCalendar, RetailerBuyingCalendarViewModel>(orderRs);
                        }

                        if(result.Retailer == null)
                        {
                            result.Retailer = rRs;
                        }

                        var item = result.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<RetailerBuyingCalendarItem, RetailerBuyingCalendarItemViewModel>(itemRs);
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
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<RetailerBuyingCalendar, Retailer, RetailerBuyingCalendarItem, Product, UoM, RetailerBuyingCalendarViewModel>(
                        (orderRs, rRs, itemRs, pRs, uRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<RetailerBuyingCalendar, RetailerBuyingCalendarViewModel>(orderRs);
                            }

                            if (result.Retailer == null)
                            {
                                result.Retailer = rRs;
                            }

                            var item = result.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<RetailerBuyingCalendarItem, RetailerBuyingCalendarItemViewModel>(itemRs);
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

        public async Task<IEnumerable<RetailerBuyingCalendarViewModel>> Gets(int retailer)
        {
            List<RetailerBuyingCalendarViewModel> result = new List<RetailerBuyingCalendarViewModel>();
            string cmd = $@"SELECT * FROM `retailer_buying_calendar` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_buying_calendar_item` i ON i.retailer_buying_calendar_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.`retailer_id` = {retailer} AND o.is_deleted = 0 ORDER BY o.created_date";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerBuyingCalendar, Retailer, RetailerBuyingCalendarItem, Product, UoM, RetailerBuyingCalendarViewModel>(
                    (orderRs, rRs, itemRs, pRs, uRs) =>
                    {
                        var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                        if (order == null)
                        {
                            order = CommonHelper.Mapper<RetailerBuyingCalendar, RetailerBuyingCalendarViewModel>(orderRs);
                            result.Add(order);
                        }

                        if (order.Retailer == null)
                        {
                            order.Retailer = rRs;
                        }

                        var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<RetailerBuyingCalendarItem, RetailerBuyingCalendarItemViewModel>(itemRs);
                            order.Items.Add(item);
                        }

                        item.Product = pRs;
                        item.UoM = uRs;

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
                    rd.Read<RetailerBuyingCalendar, Retailer, RetailerBuyingCalendarItem, Product, UoM, RetailerBuyingCalendarViewModel>(
                        (orderRs, rRs, itemRs, pRs, uRs) =>
                        {
                            var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                            if (order == null)
                            {
                                order = CommonHelper.Mapper<RetailerBuyingCalendar, RetailerBuyingCalendarViewModel>(orderRs);
                                result.Add(order);
                            }

                            if (order.Retailer == null)
                            {
                                order.Retailer = rRs;
                            }

                            var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<RetailerBuyingCalendarItem, RetailerBuyingCalendarItemViewModel>(itemRs);
                                order.Items.Add(item);
                            }

                            item.Product = pRs;
                            item.UoM = uRs;

                            return order;
                        }
                    );

                    return result;
                }
            }
        }

        public async Task<IEnumerable<RetailerBuyingCalendarViewModel>> Gets(string condition = "")
        {
            List<RetailerBuyingCalendarViewModel> result = new List<RetailerBuyingCalendarViewModel>();
            string cmd = $@"SELECT * FROM `retailer_buying_calendar` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_buying_calendar_item` i ON i.retailer_buying_calendar_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.is_deleted = 0 ORDER BY o.created_date";
            if(!string.IsNullOrEmpty(condition))
            {
                cmd += $" AND {condition}";
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerBuyingCalendar, Retailer, RetailerBuyingCalendarItem, Product, UoM, RetailerBuyingCalendarViewModel>(
                    (orderRs, rRs, itemRs, pRs, uRs) =>
                    {
                        var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                        if (order == null)
                        {
                            order = CommonHelper.Mapper<RetailerBuyingCalendar, RetailerBuyingCalendarViewModel>(orderRs);
                            result.Add(order);
                        }

                        if (order.Retailer == null)
                        {
                            order.Retailer = rRs;
                        }

                        var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<RetailerBuyingCalendarItem, RetailerBuyingCalendarItemViewModel>(itemRs);
                            order.Items.Add(item);
                        }

                        item.Product = pRs;
                        item.UoM = uRs;

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
                    rd.Read<RetailerBuyingCalendar, Retailer, RetailerBuyingCalendarItem, Product, UoM, RetailerBuyingCalendarViewModel>(
                        (orderRs, rRs, itemRs, pRs, uRs) =>
                        {
                            var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                            if (order == null)
                            {
                                order = CommonHelper.Mapper<RetailerBuyingCalendar, RetailerBuyingCalendarViewModel>(orderRs);
                                result.Add(order);
                            }

                            if (order.Retailer == null)
                            {
                                order.Retailer = rRs;
                            }

                            var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<RetailerBuyingCalendarItem, RetailerBuyingCalendarItemViewModel>(itemRs);
                                order.Items.Add(item);
                            }

                            item.Product = pRs;
                            item.UoM = uRs;

                            return order;
                        }
                    );

                    return result;
                }
            }
        }
    }
}
