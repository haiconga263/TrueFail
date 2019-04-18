using Web.Controllers;

namespace Collections.Commands.CollectionEmployees
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
