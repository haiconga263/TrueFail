using Productions.UI.Pesticides.Models;
using Web.Controllers;

namespace Productions.Pesticides.Commands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Pesticide Pesticide { set; get; }
        public UpdateCommand(Pesticide pesticide)
        {
            Pesticide = pesticide;
        }
    }
}
