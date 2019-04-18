using MDM.UI.Retailers.ViewModels;
using Web.Controllers;

namespace Retailers.Commands.Commands.Retailers
{
    public class AddCommand : BaseCommand<int>
    {
        public RetailerViewModel Retailer { set; get; }
        public AddCommand(RetailerViewModel retailer)
        {
            Retailer = retailer;
        }
    }
}
