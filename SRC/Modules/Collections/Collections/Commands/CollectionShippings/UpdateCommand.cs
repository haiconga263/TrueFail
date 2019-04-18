using MDM.UI.Collections.ViewModels;
using MDM.UI.Common.Models;
using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Products.ViewModels;
using Web.Controllers;

namespace Collections.Commands.CollectionShippings
{
    public class UpdateCommand : BaseCommand<int>
    {
        public CFShipping Shipping { set; get; }
        public UpdateCommand(CFShipping shipping)
        {
            Shipping = shipping;
        }
    }
}
