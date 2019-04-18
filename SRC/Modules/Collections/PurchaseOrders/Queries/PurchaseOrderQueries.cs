using Collections.UI.PurchaseOrders.Interfaces;
using Collections.UI.PurchaseOrders.Models;
using Collections.UI.PurchaseOrders.ViewModels;
using Common.Models;
using DAL;
using Dapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;

namespace Collections.PurchaseOrders.Queries
{
    public class PurchaseOrderQueries : BaseQueries, IPurchaseOrderQueries
    {
        public async Task<PurchaseOrderInformation> GetByDateCodeAsync(string datecode)
        {
            string cmd = $@"SELECT * FROM aritnt.`collection_purchase_order` po
	                        LEFT JOIN aritnt.`collection_purchase_order_item` poi ON poi.`purchase_order_id` = po.`id`
                            WHERE po.`code` = '{datecode}' AND po.`is_deleted` = '0'";

            DbConnection = DbConnection ?? DALHelper.GetConnection();

            List<PurchaseOrderInformation> result = new List<PurchaseOrderInformation>();

            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            rd.Read<PurchaseOrder, PurchaseOrderItem, int>(
                (poRs, poItemRs) =>
                {
                    var po = result.Find(x => x.Id == poRs.Id);
                    if (po == null)
                    {
                        po = CommonHelper.Mapper<PurchaseOrder, PurchaseOrderInformation>(poRs);
                        result.Add(po);
                    }

                    if (po.Items == null)
                        po.Items = new List<PurchaseOrderItem>();

                    po.Items.Add(poItemRs);
                    return 0;
                }
            );

            return result.FirstOrDefault();
        }

        public async Task<PurchaseOrderInformation> GetByIdAsync(long id)
        {
            string cmd = $@"SELECT * FROM aritnt.`collection_purchase_order` po
	                        LEFT JOIN aritnt.`collection_purchase_order_item` poi ON poi.`purchase_order_id` = po.`id`
                            WHERE po.`id` = '{id}' AND po.`is_deleted` = '0'";

            DbConnection = DbConnection ?? DALHelper.GetConnection();

            List<PurchaseOrderInformation> result = new List<PurchaseOrderInformation>();

            var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
            rd.Read<PurchaseOrder, PurchaseOrderItem, int>(
                (poRs, poItemRs) =>
                {
                    var po = result.Find(x => x.Id == poRs.Id);
                    if (po == null)
                    {
                        po = CommonHelper.Mapper<PurchaseOrder, PurchaseOrderInformation>(poRs);
                        result.Add(po);
                    }

                    if (po.Items == null)
                        po.Items = new List<PurchaseOrderItem>();

                    po.Items.Add(poItemRs);
                    return 0;
                }
            );

            return result.FirstOrDefault();
        }

        public async Task<PurchaseOrderItem> GetItemByItemIdAsync(long itemId)
        {
            string cmd = $"SELECT * FROM aritnt.`collection_purchase_order_item` poi WHERE poi.`id` = '{itemId}'";
            return (await DALHelper.Query<PurchaseOrderItem>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<PurchaseOrder>> GetsAsync()
        {
            string cmd = $"SELECT * FROM aritnt.`collection_purchase_order` po WHERE po.`is_deleted` = '0'";
            return await DALHelper.Query<PurchaseOrder>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetsAsync(DateTime lastRequest)
        {
            string cmd = $@"SELECT * FROM aritnt.`collection_purchase_order` po
		                        WHERE po.`is_deleted` = '0' AND po.created_date >= '{lastRequest.ToString("yyyy-MM-dd HH:mm:ss")}'";
            return await DALHelper.Query<PurchaseOrder>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
