using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Products.ViewModels;
using Web.Controllers;

namespace Admin.Commands.ProductCommand
{
    public class UpdateCommand : BaseCommand<int>
    {
        public ProductViewModel Product { set; get; }
        public UpdateCommand(ProductViewModel product)
        {
            Product = product;
        }
    }
}
