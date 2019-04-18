using Web.Controllers;

namespace Productions.Plots.Commands
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int Id { set; get; }
        public DeleteCommand(int id)
        {
            Id = id;
        }
    }
}
