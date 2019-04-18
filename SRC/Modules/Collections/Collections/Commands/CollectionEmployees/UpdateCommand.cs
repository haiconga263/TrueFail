using MDM.UI.Collections.Models;
using Web.Controllers;

namespace Collections.Commands.CollectionEmployees
{
    public class UpdateCommand : BaseCommand<int>
    {
        public CollectionEmployee Employee { set; get; }
        public UpdateCommand(CollectionEmployee employee)
        {
            Employee = employee;
        }
    }
}
