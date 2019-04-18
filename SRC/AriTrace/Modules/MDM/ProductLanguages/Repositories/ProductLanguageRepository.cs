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

namespace MDM.ProductLanguages.Repositories
{
    public class ProductLanguageRepository : BaseRepository, IProductLanguageRepository
    {
        public async Task<int> AddAsync(ProductLanguage productLanguage)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(productLanguage);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `product_language` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(ProductLanguage productLanguage)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(productLanguage);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
