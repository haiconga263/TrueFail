using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.ProductCommand
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int ProductId { set; get; }
        public DeleteCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
