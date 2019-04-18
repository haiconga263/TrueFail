using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Retailers.Commands.Commands.Retailers
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int RetailerId { set; get; }
        public DeleteCommand(int retailerId)
        {
            RetailerId = retailerId;
        }
    }
}
