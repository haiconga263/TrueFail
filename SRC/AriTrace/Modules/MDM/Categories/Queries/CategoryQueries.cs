using Common.Models;
using DAL;
using MDM.UI.Categories.Interfaces;
using MDM.UI.Categories.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Categories.Queries
{
    public class CategoryQueries : BaseQueries, ICategoryQueries
    {
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `category` WHERE `is_deleted` = 0";
            return await DALHelper.Query<Category>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<Category>($"SELECT * FROM `category` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Category>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `category` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<Category>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
