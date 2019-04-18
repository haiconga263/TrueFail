using Web.Controllers;

namespace Productions.Pesticides.Commands.PesticideCategories
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
