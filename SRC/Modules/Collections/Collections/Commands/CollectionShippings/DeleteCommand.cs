using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Collections.Commands.CollectionShippings
{
    public class DeleteCommand : BaseCommand<int>
    {
        public long ShippingId { set; get; }
        public DeleteCommand(long shippingId)
        {
            ShippingId = shippingId;
        }
    }
}
