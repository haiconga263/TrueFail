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

namespace Production.CultureFields.Repositories
{
    public class CultureFieldRepository : BaseRepository, ICultureFieldRepository
    {
        public async Task<int> AddAsync(CultureField cultureField)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(cultureField);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `culture_field` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(CultureField cultureField)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(cultureField);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
