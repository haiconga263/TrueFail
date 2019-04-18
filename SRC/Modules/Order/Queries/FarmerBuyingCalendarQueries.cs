using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using MDM.UI.Farmers.Models;
using MDM.UI.Products.Models;
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
    public class FarmerBuyingCalendarQueries : BaseQueries, IFarmerBuyingCalendarQueries
    {
        private const string OrderCodeFormat = "P{y}{n}"; // O<year><number>

        public async Task<bool> CheckChangedByFarmer(int farmerId, DateTime lastRequest)
        {
            string cmd = $@"SELECT * FROM `farmer_buying_calendar` fb
                            WHERE fb.`farmer_id` = '{farmerId}' AND 
                            (( fb.`modified_date`IS NOT NULL AND fb.`modified_date` >= '{lastRequest.ToString("yyyy-MM-dd HH:mm:ss")}' ) 
	                            OR ( fb.`modified_date` IS NULL AND fb.`created_date` >= '{lastRequest.ToString("yyyy-MM-dd HH:mm:ss")}' ))";

            DbConnection = DbConnection ?? DALHelper.GetConnection();

            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            return rd.Read<Farmer>().Count() > 0;
        }

        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `farmer_buying_calendar`", dbTransaction: DbTransaction, connection: DbConnection);
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

        public async Task<FarmerBuyingCalendarViewModel> Get(long id)
        {
            FarmerBuyingCalendarViewModel result = null;
            string cmd = $@"SELECT * FROM `farmer_buying_calendar` o
                            LEFT JOIN `farmer` f ON o.farmer_id = f.id AND f.is_used = 1 AND f.is_deleted = 0
                            LEFT JOIN `farmer_buying_calendar_item` i ON i.farmer_buying_calendar_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.`id` = {id} AND o.is_deleted = 0";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<FarmerBuyingCalendar, Farmer, FarmerBuyingCalendarItem, Product, UoM, FarmerBuyingCalendarViewModel>(
                    (orderRs, fRs, itemRs, pRs, uRs) =>
                    {
                        if (result == null)
                        {
                            result = CommonHelper.Mapper<FarmerBuyingCalendar, FarmerBuyingCalendarViewModel>(orderRs);
                        }

                        if (result.Farmer == null)
                        {
                            result.Farmer = fRs;
                        }

                        var item = result.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<FarmerBuyingCalendarItem, FarmerBuyingCalendarItemViewModel>(itemRs);
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
                    rd.Read<FarmerBuyingCalendar, Farmer, FarmerBuyingCalendarItem, Product, UoM, FarmerBuyingCalendarViewModel>(
                        (orderRs, fRs, itemRs, pRs, uRs) =>
                        {
                            if (result == null)
                            {
                                result = CommonHelper.Mapper<FarmerBuyingCalendar, FarmerBuyingCalendarViewModel>(orderRs);
                            }

                            if (result.Farmer == null)
                            {
                                result.Farmer = fRs;
                            }

                            var item = result.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<FarmerBuyingCalendarItem, FarmerBuyingCalendarItemViewModel>(itemRs);
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

        public async Task<IEnumerable<FarmerBuyingCalendarViewModel>> Gets(int farmerId)
        {
            List<FarmerBuyingCalendarViewModel> result = new List<FarmerBuyingCalendarViewModel>();
            string cmd = $@"SELECT * FROM `farmer_buying_calendar` o
                            LEFT JOIN `farmer` f ON o.farmer_id = f.id AND f.is_used = 1 AND f.is_deleted = 0
                            LEFT JOIN `farmer_buying_calendar_item` i ON i.farmer_buying_calendar_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.`farmer_id` = {farmerId} AND o.is_deleted = 0";
            if (DbConnection != null)
            {
                SqlMapper.GridReader rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<FarmerBuyingCalendar, Farmer, FarmerBuyingCalendarItem, Product, UoM, FarmerBuyingCalendarViewModel>(
                    (orderRs, fRs, itemRs, pRs, uRs) =>
                    {
                        var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                        if (order == null)
                        {
                            order = CommonHelper.Mapper<FarmerBuyingCalendar, FarmerBuyingCalendarViewModel>(orderRs);
                            result.Add(order);
                        }

                        if (order.Farmer == null)
                        {
                            order.Farmer = fRs;
                        }

                        var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<FarmerBuyingCalendarItem, FarmerBuyingCalendarItemViewModel>(itemRs);
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
                    SqlMapper.GridReader rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<FarmerBuyingCalendar, Farmer, FarmerBuyingCalendarItem, Product, UoM, FarmerBuyingCalendarViewModel>(
                        (orderRs, fRs, itemRs, pRs, uRs) =>
                        {
                            var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                            if (order == null)
                            {
                                order = CommonHelper.Mapper<FarmerBuyingCalendar, FarmerBuyingCalendarViewModel>(orderRs);
                                result.Add(order);
                            }

                            if (order.Farmer == null)
                            {
                                order.Farmer = fRs;
                            }

                            var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<FarmerBuyingCalendarItem, FarmerBuyingCalendarItemViewModel>(itemRs);
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

        public async Task<IEnumerable<FarmerBuyingCalendarViewModel>> Gets(string condition = "")
        {
            List<FarmerBuyingCalendarViewModel> result = new List<FarmerBuyingCalendarViewModel>();
            string cmd = $@"SELECT * FROM `farmer_buying_calendar` o
                            LEFT JOIN `farmer` f ON o.farmer_id = f.id AND f.is_used = 1 AND f.is_deleted = 0
                            LEFT JOIN `farmer_buying_calendar_item` i ON i.farmer_buying_calendar_id = o.id
                            LEFT JOIN `product` p ON p.id = i.product_id AND p.is_used = 1 AND p.is_deleted = 0
                            LEFT JOIN `uom` u ON u.id = i.uom_id AND u.is_used = 1 AND u.is_deleted = 0
                            WHERE o.is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            if (DbConnection != null)
            {
                SqlMapper.GridReader rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<FarmerBuyingCalendar, Farmer, FarmerBuyingCalendarItem, Product, UoM, FarmerBuyingCalendarViewModel>(
                    (orderRs, fRs, itemRs, pRs, uRs) =>
                    {
                        var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                        if (order == null)
                        {
                            order = CommonHelper.Mapper<FarmerBuyingCalendar, FarmerBuyingCalendarViewModel>(orderRs);
                            result.Add(order);
                        }

                        if (order.Farmer == null)
                        {
                            order.Farmer = fRs;
                        }

                        var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                        if (item == null)
                        {
                            item = CommonHelper.Mapper<FarmerBuyingCalendarItem, FarmerBuyingCalendarItemViewModel>(itemRs);
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
                    SqlMapper.GridReader rd = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction);
                    rd.Read<FarmerBuyingCalendar, Farmer, FarmerBuyingCalendarItem, Product, UoM, FarmerBuyingCalendarViewModel>(
                        (orderRs, fRs, itemRs, pRs, uRs) =>
                        {
                            var order = result.FirstOrDefault(o => o.Id == orderRs.Id);

                            if (order == null)
                            {
                                order = CommonHelper.Mapper<FarmerBuyingCalendar, FarmerBuyingCalendarViewModel>(orderRs);
                                result.Add(order);
                            }

                            if (order.Farmer == null)
                            {
                                order.Farmer = fRs;
                            }

                            var item = order.Items.FirstOrDefault(i => i.Id == itemRs.Id);
                            if (item == null)
                            {
                                item = CommonHelper.Mapper<FarmerBuyingCalendarItem, FarmerBuyingCalendarItemViewModel>(itemRs);
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
