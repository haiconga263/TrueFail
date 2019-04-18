using Productions.UI.Cultivations.Models;
using Web.Controllers;

namespace Productions.Cultivations.Commands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Cultivation Cultivation { set; get; }
        public UpdateCommand(Cultivation cultivation)
        {
            Cultivation = cultivation;
        }
    }
}
