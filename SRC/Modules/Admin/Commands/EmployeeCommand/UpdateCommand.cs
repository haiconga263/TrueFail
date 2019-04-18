using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using Web.Controllers;

namespace Admin.Commands.EmployeeCommand
{
    public class UpdateCommand : BaseCommand<int>
    {
        public EmployeeViewModel Employee { set; get; }
        public UpdateCommand(EmployeeViewModel employee)
        {
            Employee = employee;
        }
    }
}
