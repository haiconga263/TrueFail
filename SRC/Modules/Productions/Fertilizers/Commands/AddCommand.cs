using Productions.UI.Fertilizers.Models;
using Web.Controllers;

namespace Productions.Fertilizers.Commands
{
    public class AddCommand : BaseCommand<int>
    {
        public Fertilizer Fertilizer { set; get; }
        public AddCommand(Fertilizer fertilizer)
        {
            Fertilizer = fertilizer;
        }
    }
}
