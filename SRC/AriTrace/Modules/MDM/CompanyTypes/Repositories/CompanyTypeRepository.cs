using Common.Models;
using DAL;
using MDM.UI.CompanyTypes.Interfaces;
using MDM.UI.CompanyTypes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.CompanyTypes.Repositories
{
    public class CompanyTypeRepository : BaseRepository, ICompanyTypeRepository
    {
        public async Task<int> AddAsync(CompanyType companyType)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(companyType);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cmd = $"delete from `compan_type` WHERE `id` = {id}";
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> UpdateAsync(CompanyType companyType)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(companyType);
            return await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
