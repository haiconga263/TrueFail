using Common.Models;
using DAL;
using Dapper;
using MDM.UI.ProductLanguages.Models;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.Models;
using MDM.UI.Products.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.Products.Queries
{
    public class ProductQueries : BaseQueries, IProductQueries
    {
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            string cmd = $@"SELECT * FROM `product` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Product>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<ProductMuiltipleLanguage> GetByIdAsync(int id)
        {
            string cmd = $@"SELECT p.*, pl.* FROM aritrace.`product` p
	                        LEFT JOIN aritrace.`product_language` pl ON p.id = pl.product_id
	                        WHERE p.`id` = '{id}' and p.`is_deleted` = 0";

            var conn = DbConnection;
            if (conn == null)
                conn = DALHelper.GetConnection();
            try
            {
                ProductMuiltipleLanguage product = null;
                using (var reader = await conn.QueryMultipleAsync(cmd, transaction: DbTransaction))
                {
                    return reader.Read<Product, ProductLanguage, ProductMuiltipleLanguage>(
                           (productRs, productLanguageRs) =>
                           {
                               if (product == null)
                               {
                                   var serializedParent = JsonConvert.SerializeObject(productRs);
                                   product = JsonConvert.DeserializeObject<ProductMuiltipleLanguage>(serializedParent);
                               }
                               if (productLanguageRs != null)
                               {
                                   if (product.ProductLanguages == null)
                                       product.ProductLanguages = new List<ProductLanguage>();
                                   product.ProductLanguages.Add(productLanguageRs);
                               }
                               return product;
                           }
                       ).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (DbConnection == null) conn.Dispose();
            }
        }

        public async Task<IEnumerable<Product>> GetsAsync()
        {
            string cmd = $@"SELECT * FROM `product` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Product>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
