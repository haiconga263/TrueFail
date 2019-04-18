using Productions.UI.Cultivations.Models;
using Web.Controllers;

namespace Productions.Cultivations.Commands
{
    public class AddCommand : BaseCommand<int>
    {
        public Cultivation Cultivation { set; get; }
        public AddCommand(Cultivation cultivation)
        {
            Cultivation = cultivation;
        }
    }
}
