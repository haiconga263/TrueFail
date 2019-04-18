using Common.Models;
using DAL;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MDM.Employees.Repositories
{
    public class EmployeeRepository :  BaseRepository, IEmployeeRepository
    {
        public async Task<int> Add(Employee employee)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(employee);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Employee employee)
        {
            var cmd = $@"UPDATE `employee`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {employee.ModifiedBy},
                         `modified_date` = '{employee.ModifiedDate?.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {employee.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Employee employee)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(employee);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
