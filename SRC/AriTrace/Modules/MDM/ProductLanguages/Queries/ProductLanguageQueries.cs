using Common.Models;
using DAL;
using MDM.UI.ProductLanguages.Interfaces;
using MDM.UI.ProductLanguages.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.ProductLanguages.Queries
{
    public class ProductLanguageQueries : BaseQueries, IProductLanguageQueries
    {
        public async Task<IEnumerable<ProductLanguage>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `product_language` WHERE `is_deleted` = 0";
            return await DALHelper.Query<ProductLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<ProductLanguage> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<ProductLanguage>($"SELECT * FROM `product_language` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<ProductLanguage>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `product_language` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<ProductLanguage>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
