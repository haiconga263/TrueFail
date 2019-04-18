using Productions.UI.Seeds.Models;
using Web.Controllers;

namespace Productions.Seeds.Commands
{
    public class AddCommand : BaseCommand<int>
    {
        public Seed Seed { set; get; }
        public AddCommand(Seed seed)
        {
            Seed = seed;
        }
    }
}
