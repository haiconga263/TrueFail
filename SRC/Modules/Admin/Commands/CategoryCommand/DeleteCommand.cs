using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.CategoryCommand
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int CategoryId { set; get; }
        public DeleteCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
