using Common.Models;
using DAL;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Products.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public async Task<int> Add(Product product)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(product);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> AddOrUpdateLanguage(ProductLanguage language)
        {
            string cmd = QueriesCreatingHelper.CreateQuerySelect<ProductLanguage>($"product_id = {language.ProductId} AND language_id = {language.LanguageId}");
            var lang = (await DALHelper.ExecuteQuery<ProductLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
            if(lang == null)
            {
                cmd = QueriesCreatingHelper.CreateQueryInsert(language);
                cmd += ";SELECT LAST_INSERT_ID();";
                return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            }
            else
            {
                language.Id = lang.Id;
                cmd = QueriesCreatingHelper.CreateQueryUpdate(language);
                var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
                return rs == 0 ? -1 : language.Id;
            }
        }

        public async Task<int> AddPrice(ProductPrice price)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(price);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Product product)
        {
            var cmd = $@"UPDATE `product`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {product.ModifiedBy},
                         `modified_date` = '{product.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {product.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Product product)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(product);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> UpdatePrice(ProductPrice price)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(price);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
