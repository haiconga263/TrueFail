using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Categories.Models;
using MDM.UI.Categories.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.CategoryCommand
{
    public class AddCommand : BaseCommand<int>
    {
        public CategoryLanguageViewModel Category { set; get; }
        public AddCommand(CategoryLanguageViewModel category)
        {
            Category = category;
        }
    }
}
