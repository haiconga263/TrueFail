using Productions.UI.Plots.Models;
using Web.Controllers;

namespace Productions.Plots.Commands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Plot Plot { set; get; }
        public UpdateCommand(Plot plot)
        {
            Plot = plot;
        }
    }
}
