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

namespace MDM.CompanyTypes.Queries
{
    public class CompanyTypeQueries : BaseQueries, ICompanyTypeQueries
    {
        public async Task<IEnumerable<CompanyType>> GetAllAsync()
        {
            string cmd = $"SELECT * FROM `company_type` WHERE `is_deleted` = 0";
            return await DALHelper.Query<CompanyType>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<CompanyType> GetByIdAsync(int id)
        {
            return (await DALHelper.Query<CompanyType>($"SELECT * FROM `company_type` WHERE `id` = {id}", dbTransaction: DbTransaction, connection: DbConnection))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<CompanyType>> GetsAsync()
        {
            string cmd = $"SELECT * FROM `company_type` WHERE `is_used` = 1 AND `is_deleted` = 0";
            return await DALHelper.Query<CompanyType>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }

    }
}
