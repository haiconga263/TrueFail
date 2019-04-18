using Common.Models;
using DAL;
using MDM.UI.Common.Interfaces;
using MDM.UI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Common.Queries
{
    public class CFShippingQueries : BaseQueries, ICFShippingQueries
    {
        private const string ShippingCodeFormat = "S{0}";
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `cf_shipping`");
            if (previousCode == null)
            {
                code = ShippingCodeFormat.Replace("{0}", 1.ToString("000000000"));
            }
            else
            {
                code = ShippingCodeFormat.Replace("{0}", (Int32.Parse(previousCode.Substring(1, 9)) + 1).ToString("000000000"));
            }

            return code;
        }

        public async Task<CFShipping> Get(long shippingId)
        {
            return (await DALHelper.Query<CFShipping>($"SELECT * FROM `cf_shipping` WHERE `id` = {shippingId} AND is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<CFShippingItem> GetItem(long shippingItemId)
        {
            string cmd = $"SELECT * FROM `cf_shipping_item` WHERE id = {shippingItemId}";
            return (await DALHelper.Query<CFShippingItem>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<CFShippingItem>> GetItems(long shippingId)
        {
            string cmd = $"SELECT * FROM `cf_shipping_item` WHERE cf_shipping_id = {shippingId}";
            return await DALHelper.Query<CFShippingItem>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<CFShippingItem>> GetItems(string condition = "")
        {
            string cmd = "SELECT * FROM `cf_shipping_item`";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            return await DALHelper.Query<CFShippingItem>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<CFShipping>> Gets(string condition = "")
        {
            string cmd = "SELECT * FROM `cf_shipping` WHERE is_deleted = 0";
            if (!string.IsNullOrEmpty(condition))
            {
                cmd += " AND " + condition;
            }
            return await DALHelper.Query<CFShipping>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
