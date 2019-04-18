using Common.ViewModels;
using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using Web.Controllers;

namespace Admin.Commands.LanguageCommand
{
    public class UpdateCommand : BaseCommand<int>
    {
        public CaptionViewModel Caption { set; get; }
        public UpdateCommand(CaptionViewModel caption)
        {
            Caption = caption;
        }
    }
}
