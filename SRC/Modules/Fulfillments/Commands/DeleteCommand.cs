using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Fulfillments.Commands
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int FulfillmentId { set; get; }
        public DeleteCommand(int fulfillmentId)
        {
            FulfillmentId = fulfillmentId;
        }
    }
}
