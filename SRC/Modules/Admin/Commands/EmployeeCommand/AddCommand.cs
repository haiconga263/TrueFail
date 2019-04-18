using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.EmployeeCommand
{
    public class AddCommand : BaseCommand<int>
    {
        public EmployeeViewModel Employee { set; get; }
        public AddCommand(EmployeeViewModel employee)
        {
            Employee = employee;
        }
    }
}
