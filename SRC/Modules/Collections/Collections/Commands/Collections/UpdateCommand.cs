using MDM.UI.Collections.ViewModels;
using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Products.ViewModels;
using Web.Controllers;

namespace Collections.Commands.Collections
{
    public class UpdateCommand : BaseCommand<int>
    {
        public CollectionViewModel Collection { set; get; }
        public UpdateCommand(CollectionViewModel collection)
        {
            Collection = collection;
        }
    }
}
