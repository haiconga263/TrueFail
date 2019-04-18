using Common.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Employees.Interfaces
{
    public interface IEmployeeQueries : IBaseQueries
    {
        Task<IEnumerable<Employee>> GetEmployees(string condition = "");
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> GetEmployee(string code);
        Task<Employee> GetEmployeeBySupervisor(int supervisorId);
        Task<Employee> GetEmployeeBySupervisor(string supervisorCode);
        Task<string> GenarateCode();
    }
}
