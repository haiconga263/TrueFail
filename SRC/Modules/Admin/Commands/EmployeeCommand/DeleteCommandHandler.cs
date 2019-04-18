using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.EmployeeCommand
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IEmployeeRepository employeeRepository = null;
        public DeleteCommandHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await employeeRepository.Delete(DeleteBuild(new Employee()
            {
                Id = request.EmployeeId
            }, request.LoginSession));
        }
    }
}
