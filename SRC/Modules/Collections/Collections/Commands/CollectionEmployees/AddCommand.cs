using MDM.UI.Collections.Models;
using Web.Controllers;

namespace Collections.Commands.CollectionEmployees
{
    public class AddCommand : BaseCommand<int>
    {
        public CollectionEmployee Employee { set; get; }
        public AddCommand(CollectionEmployee employee)
        {
            Employee = employee;
        }
    }
}
