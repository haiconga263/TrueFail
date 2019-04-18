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

namespace GS1.ProductionImages.Queries
{
    public class ProductionImageQueries : BaseQueries, IProductionImageQueries
    {
        public async Task<IEnumerable<ProductionImage>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `production_image` WHERE `is_deleted` = 0";
            return await DALHelper.Query<ProductionImage>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<ProductionImage> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<ProductionImage>($"SELECT * FROM `production_image` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<ProductionImage>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `production_image` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<ProductionImage>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
