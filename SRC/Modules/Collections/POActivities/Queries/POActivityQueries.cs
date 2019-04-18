using Collections.UI.POActivities.Interfaces;
using Collections.UI.POActivities.Models;
using Collections.UI.POActivities.ViewModels;
using Common.Helpers;
using Common.Models;
using DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collections.POActivities.Queries
{
    public class POActivityQueries : BaseQueries, IPOActivityQueries
    {
        public async Task<POActivityInformation> GetByIdAsync(int id)
        {
            string cmd = $@"SELECT poact.*, f.`name` as 'farmer_name', ua.`user_name` as 'collector_name' 
		                        FROM aritnt.`collection_purchase_order_activity` poact
		                        LEFT JOIN aritnt.`farmer` f ON poact.`farmer_id` = f.`id`
                                LEFT JOIN aritnt.`user_account` ua ON poact.`collector_id` = ua.`id`
		                        WHERE poact.`id` = '{id}'";
            return (await DALHelper.Query<POActivityInformation>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<POActivityInformation>> GetByPOIdAsync(int poId)
        {
            string cmd = $@"SELECT poact.*, f.`name` as 'farmer_name', ua.`user_name` as 'collector_name' 
		                        FROM aritnt.`collection_purchase_order_activity` poact
		                        LEFT JOIN aritnt.`farmer` f ON poact.`farmer_id` = f.`id`
                                LEFT JOIN aritnt.`user_account` ua ON poact.`collector_id` = ua.`id`
		                        WHERE poact.`purchase_order_item_id` = '{poId}'";
            return await DALHelper.Query<POActivityInformation>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<POActivityInformation>> GetsAsync(DateTime lastRequest)
        {
            string cmd = $@"SELECT poact.*, f.`name` as 'farmer_name', ua.`user_name` as 'collector_name' 
		                        FROM aritnt.`collection_purchase_order_activity` poact
		                        LEFT JOIN aritnt.`farmer` f ON poact.`farmer_id` = f.`id`
                                LEFT JOIN aritnt.`user_account` ua ON poact.`collector_id` = ua.`id`
		                        WHERE poact.`created_date` >= '{lastRequest.ToString("yyyy-MM-dd HH:mm:ss")}'";
            return await DALHelper.Query<POActivityInformation>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
