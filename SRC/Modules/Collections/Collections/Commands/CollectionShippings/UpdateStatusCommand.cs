using MDM.UI.Collections.ViewModels;
using MDM.UI.Common.Models;
using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Products.ViewModels;
using Web.Controllers;

namespace Collections.Commands.CollectionShippings
{
    public class UpdateStatusCommand : BaseCommand<int>
    {
        public long ShippingId { set; get; }
        public short StatusId { set; get; }
        public UpdateStatusCommand(long shippingId, short statusId)
        {
            ShippingId = shippingId;
            StatusId = statusId;
        }
    }
}
