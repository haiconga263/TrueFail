using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Categories.ViewModels;
using Web.Controllers;

namespace Admin.Commands.CategoryCommand
{
    public class UpdateCommand : BaseCommand<int>
    {
        public CategoryLanguageViewModel Category { set; get; }
        public UpdateCommand(CategoryLanguageViewModel category)
        {
            Category = category;
        }
    }
}
