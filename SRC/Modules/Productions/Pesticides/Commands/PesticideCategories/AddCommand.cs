using Productions.UI.Pesticides.Models;
using Web.Controllers;

namespace Productions.Pesticides.Commands.PesticideCategories
{
    public class AddCommand : BaseCommand<int>
    {
        public PesticideCategory PesticideCategory { set; get; }
        public AddCommand(PesticideCategory pesticideCategory)
        {
            PesticideCategory = pesticideCategory;
        }
    }
}
