using Common.Models;
using DAL;
using MDM.UI.Companies.Interfaces;
using MDM.UI.Companies.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Companies.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public async Task<int> AddAsync(Company company)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(company);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `company` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(Company company)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(company);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
