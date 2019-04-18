using Common.Models;
using DAL;
using GS1.UI.ProductionImages.Interfaces;
using GS1.UI.ProductionImages.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1.ProductionImages.Repositories
{
    public class ProductionImageRepository : BaseRepository, IProductionImageRepository
    {
        public async Task<int> AddAsync(ProductionImage productionImage)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(productionImage);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `production_image` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(ProductionImage productionImage)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(productionImage);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
