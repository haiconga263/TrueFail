using Productions.UI.Pesticides.Models;
using Web.Controllers;

namespace Productions.Pesticides.Commands
{
    public class AddCommand : BaseCommand<int>
    {
        public Pesticide Pesticide { set; get; }
        public AddCommand(Pesticide pesticide)
        {
            Pesticide = pesticide;
        }
    }
}
