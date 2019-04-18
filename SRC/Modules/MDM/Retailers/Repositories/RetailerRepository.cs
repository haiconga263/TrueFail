using Common.Models;
using DAL;
using MDM.UI.Retailers.Interfaces;
using MDM.UI.Retailers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Retailers.Repositories
{
    public class RetailerRepository : BaseRepository, IRetailerRepository
    {
        public async Task<int> Add(Retailer retailer)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(retailer);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddLocation(RetailerLocation retailerLocation)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(retailerLocation);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Retailer retailer)
        {
            var cmd = $@"UPDATE `retailer`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {retailer.ModifiedBy},
                         `modified_date` = '{retailer.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {retailer.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> DeleteLocation(RetailerLocation retailerLocation)
        {
            var cmd = $@"UPDATE `retailer_location`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {retailerLocation.ModifiedBy},
                         `modified_date` = '{retailerLocation.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {retailerLocation.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0; 
        }

        public async Task<int> Update(Retailer retailer)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(retailer);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> UpdateLocation(RetailerLocation retailerLocation)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(retailerLocation);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
