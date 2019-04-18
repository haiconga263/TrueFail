using Productions.UI.Pesticides.Models;
using Web.Controllers;

namespace Productions.Pesticides.Commands.PesticideCategories
{
    public class UpdateCommand : BaseCommand<int>
    {
        public PesticideCategory PesticideCategory { set; get; }
        public UpdateCommand(PesticideCategory pesticideCategory)
        {
            PesticideCategory = pesticideCategory;
        }
    }
}
