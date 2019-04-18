using Web.Controllers;

namespace Productions.Fertilizers.Commands.FertilizerCategories
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
