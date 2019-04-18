using Common.Models;
using DAL;
using Production.UI.CultureFields.Interfaces;
using Production.UI.CultureFields.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.CultureFields.Queries
{
    public class CultureFieldQueries : BaseQueries, ICultureFieldQueries
    {
        public async Task<IEnumerable<CultureField>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `culture_field` WHERE `is_deleted` = 0";
            return await DALHelper.Query<CultureField>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<CultureField> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<CultureField>($"SELECT * FROM `culture_field` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<CultureField>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `culture_field` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<CultureField>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
