using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.EmployeeCommand
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int EmployeeId { set; get; }
        public DeleteCommand(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
