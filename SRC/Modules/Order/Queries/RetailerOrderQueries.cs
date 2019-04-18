using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Employees.Models;
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
    public class RetailerOrderQueries : BaseQueries, IRetailerOrderQueries
    {
        private const string OrderCodeFormat = "O{y}{n}"; // O<year><number>
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `retailer_order`");
            if(previousCode == null)
            {
                code = OrderCodeFormat.Replace("{y}", DateTime.Now.Year.ToString("0000")).Replace("{n}", 1.ToString("00000"));
            }
            else
            {
                if(DateTime.Now.Year.ToString("0000").Equals(previousCode.Substring(1, 4)))
                {
                    code = previousCode.Substring(0, 5) + (Int32.Parse(previousCode.Substring(5, 5)) + 1).ToString("00000");
                }
            }

            return code;
        }

        public async Task<RetailerOrderViewModel> Get(long id)
        {
            RetailerOrderViewModel result = null;
            string cmd = $@"SELECT * FROM `retailer_order` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_order_item` i ON i.retailer_order_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.`id` = {id} AND o.is_deleted = 0";
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

        public async Task<IEnumerable<RetailerOrderAuditViewModel>> GetAudits(long retailerOrderId, int retailerId)
        {
            List<RetailerOrderAuditViewModel> result = new List<RetailerOrderAuditViewModel>();
            string cmd = $@"SELECT * FROM `retailer_order_audit` a
                            LEFT JOIN `employee` e ON e.is_used = 1 AND e.is_deleted = 0
                                                      and e.user_account_id = (SELECT u.id FROM `user_account` u 
                                                                               WHERE u.is_used = 1 and u.is_deleted = 0 
                                                                                     and u.id = a.created_by)
                            WHERE a.`retailer_order_id` = {retailerOrderId} AND a.`retailer_id` = {retailerId}";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerOrderAudit, Employee, RetailerOrderAuditViewModel>(
                    (aRs, eRs) =>
                    {
                        var audit = result.FirstOrDefault(a => a.Id == aRs.Id);

                        if (audit == null)
                        {
                            audit = CommonHelper.Mapper<RetailerOrderAudit, RetailerOrderAuditViewModel>(aRs);
                            result.Add(audit);
                        }

                        audit.By = eRs;

                        return audit;
                    }
                );

                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<RetailerOrderAudit, Employee, RetailerOrderAuditViewModel>(
                        (aRs, eRs) =>
                        {
                            var audit = result.FirstOrDefault(a => a.Id == aRs.Id);

                            if (audit == null)
                            {
                                audit = CommonHelper.Mapper<RetailerOrderAudit, RetailerOrderAuditViewModel>(aRs);
                                result.Add(audit);
                            }

                            audit.By = eRs;

                            return audit;
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

        public async Task<IEnumerable<RetailerOrderViewModel>> Gets(int retailer)
        {
            List<RetailerOrderViewModel> result = new List<RetailerOrderViewModel>();
            string cmd = $@"SELECT * FROM `retailer_order` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_order_item` i ON i.retailer_order_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.`retailer_id` = {retailer} AND is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerOrder, Retailer, RetailerOrderItem, Product, UoM, RetailerOrderViewModel>(
                    (orderRs, rRs, itemRs, pRs, uRs) =>
                    {
                        var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                        if (order == null)
                        {
                            order = CommonHelper.Mapper<RetailerOrder, RetailerOrderViewModel>(orderRs);
                            result.Add(order);
                        }

                        if (order.Retailer == null)
                        {
                            order.Retailer = rRs;
                        }

                        var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<RetailerOrderItem, RetailerOrderItemViewModel>(itemRs);
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
                    rd.Read<RetailerOrder, Retailer, RetailerOrderItem, Product, UoM, RetailerOrderViewModel>(
                        (orderRs, rRs, itemRs, pRs, uRs) =>
                        {
                            var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                            if (order == null)
                            {
                                order = CommonHelper.Mapper<RetailerOrder, RetailerOrderViewModel>(orderRs);
                                result.Add(order);
                            }

                            if (order.Retailer == null)
                            {
                                order.Retailer = rRs;
                            }

                            var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<RetailerOrderItem, RetailerOrderItemViewModel>(itemRs);
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

        public async Task<IEnumerable<RetailerOrderViewModel>> Gets(string condition = "")
        {
            List<RetailerOrderViewModel> result = new List<RetailerOrderViewModel>();
            string cmd = $@"SELECT * FROM `retailer_order` o
                            LEFT JOIN `retailer` r ON o.retailer_id = r.id AND r.is_used = 1 AND r.is_deleted = 0
                            LEFT JOIN `retailer_order_item` i ON i.retailer_order_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += $" AND {condition}";
            }
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<RetailerOrder, Retailer, RetailerOrderItem, Product, UoM, RetailerOrderViewModel>(
                    (orderRs, rRs, itemRs, pRs, uRs) =>
                    {
                        var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                        if (order == null)
                        {
                            order = CommonHelper.Mapper<RetailerOrder, RetailerOrderViewModel>(orderRs);
                            result.Add(order);
                        }

                        if (order.Retailer == null)
                        {
                            order.Retailer = rRs;
                        }

                        var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<RetailerOrderItem, RetailerOrderItemViewModel>(itemRs);
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
                    rd.Read<RetailerOrder, Retailer, RetailerOrderItem, Product, UoM, RetailerOrderViewModel>(
                        (orderRs, rRs, itemRs, pRs, uRs) =>
                        {
                            var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                            if (order == null)
                            {
                                order = CommonHelper.Mapper<RetailerOrder, RetailerOrderViewModel>(orderRs);
                                result.Add(order);
                            }

                            if (order.Retailer == null)
                            {
                                order.Retailer = rRs;
                            }

                            var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<RetailerOrderItem, RetailerOrderItemViewModel>(itemRs);
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
