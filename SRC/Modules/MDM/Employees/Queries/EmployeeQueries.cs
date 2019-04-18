using Common.Models;
using DAL;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Employees.Queries
{
    public class EmployeeQueries : BaseQueries, IEmployeeQueries
    {
        private const string EmployeeCodeFormat = "E{0}";
        public async Task<string> GenarateCode()
        {
            string code = string.Empty;
            var previousCode = await DALHelper.ExecuteScadar<string>("SELECT max(code) FROM `employee`");
            if (previousCode == null)
            {
                code = EmployeeCodeFormat.Replace("{0}", 1.ToString("000000000"));
            }
            else
            {
                code = EmployeeCodeFormat.Replace("{0}", (Int32.Parse(previousCode.Substring(1, 9)) + 1).ToString("000000000"));
            }

            return code;
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return (await DALHelper.Query<Employee>($"SELECT * FROM `employee` WHERE `id` = {employeeId} AND is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<Employee> GetEmployee(string code)
        {
            return (await DALHelper.Query<Employee>($"SELECT * FROM `employee` WHERE `code` = '{code}' AND is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<Employee> GetEmployeeBySupervisor(int supervisorId)
        {
            return (await DALHelper.Query<Employee>($"SELECT * FROM `employee` WHERE `report_to` = {supervisorId} AND is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<Employee> GetEmployeeBySupervisor(string supervisorCode)
        {
            return (await DALHelper.Query<Employee>($"SELECT * FROM `employee` WHERE `report_to_code` = '{supervisorCode}' AND is_deleted = 0", dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<Employee>> GetEmployees(string condition = "")
        {
            string cmd = "SELECT * FROM `employee` WHERE is_deleted = 0 AND " + (string.IsNullOrEmpty(condition) ? "1=1" : condition);
            return await DALHelper.Query<Employee>(cmd, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}
