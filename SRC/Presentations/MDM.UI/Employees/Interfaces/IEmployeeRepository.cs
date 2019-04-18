using Common.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Employees.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository
    {
        Task<int> Add(Employee employee);
        Task<int> Update(Employee employee);
        Task<int> Delete(Employee employee);
    }
}
