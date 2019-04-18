using Productions.UI.Plots.Models;
using Web.Controllers;

namespace Productions.Plots.Commands
{
    public class AddCommand : BaseCommand<int>
    {
        public Plot Plot { set; get; }
        public AddCommand(Plot plot)
        {
            Plot = plot;
        }
    }
}
