using Productions.UI.Seeds.Models;
using Web.Controllers;

namespace Productions.Seeds.Commands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Seed Seed { set; get; }
        public UpdateCommand(Seed seed)
        {
            Seed = seed;
        }
    }
}
