using Productions.UI.Fertilizers.Models;
using Web.Controllers;

namespace Productions.Fertilizers.Commands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Fertilizer Fertilizer { set; get; }
        public UpdateCommand(Fertilizer fertilizer)
        {
            Fertilizer = fertilizer;
        }
    }
}
