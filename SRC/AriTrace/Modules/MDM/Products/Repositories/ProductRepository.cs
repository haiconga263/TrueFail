using Common.Models;
using DAL;
using MDM.UI.Products.Interfaces;
using MDM.UI.Products.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Products.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public async Task<int> AddAsync(Product product)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(product);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `product` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Product product)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(product);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
